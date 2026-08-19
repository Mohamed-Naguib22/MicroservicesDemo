using InventoryService.Infrastructure.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryService.Infrastructure.Services.MessageConsumer.Common
{
    public abstract class BaseRabbitMQConsumer<TMessage> : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly RabbitMQSettings _settings;
        private readonly ConnectionFactory _factory;

        private static string QueueName => typeof(TMessage).Name;

        private IConnection? _connection;
        private IChannel? _channel;

        protected BaseRabbitMQConsumer(
            IOptions<RabbitMQSettings> options,
            ILogger logger)
        {
            _settings = options.Value;
            _logger = logger;

            _factory = new ConnectionFactory
            {
                HostName = _settings.HostName,
                UserName = _settings.UserName,
                Password = _settings.Password,
                VirtualHost = _settings.VirtualHost
            };
        }

        public override async Task StartAsync(
            CancellationToken cancellationToken)
        {
            var retryCount = 5;
            var delay = TimeSpan.FromSeconds(5);

            while (retryCount > 0)
            {
                try
                {
                    _connection ??=
                        await _factory.CreateConnectionAsync();

                    _channel ??=
                        await _connection.CreateChannelAsync();

                    await _channel.QueueDeclareAsync(
                        queue: QueueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false);

                    break;
                }
                catch (Exception ex)
                {
                    retryCount--;

                    _logger.LogWarning(
                        ex,
                        "Failed to connect to RabbitMQ. Retries left: {RetryCount}",
                        retryCount);

                    if (retryCount == 0)
                        throw;

                    await Task.Delay(delay, cancellationToken);
                }
            }

            await base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            if (_channel is null)
                throw new InvalidOperationException(
                    "RabbitMQ channel has not been initialized.");

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                try
                {
                    var message =
                        JsonSerializer.Deserialize<TMessage>(json);

                    if (message is null)
                    {
                        _logger.LogWarning(
                            "Received an empty or invalid message: {Message}",
                            json);

                        return;
                    }

                    _logger.LogInformation(
                        "Received message: {Message}",
                        json);

                    await HandleMessageAsync(message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Error handling RabbitMQ message: {Message}",
                        json);
                }
            };

            _channel.BasicConsumeAsync(
                queue: QueueName,
                autoAck: true,
                consumer: consumer);

            return Task.CompletedTask;
        }

        protected abstract Task HandleMessageAsync(TMessage message);

        public override async Task StopAsync(
            CancellationToken cancellationToken)
        {
            if (_channel is not null)
            {
                await _channel.CloseAsync();
                await _channel.DisposeAsync();
            }

            if (_connection is not null)
            {
                await _connection.CloseAsync();
                await _connection.DisposeAsync();
            }

            await base.StopAsync(cancellationToken);
        }
    }
}