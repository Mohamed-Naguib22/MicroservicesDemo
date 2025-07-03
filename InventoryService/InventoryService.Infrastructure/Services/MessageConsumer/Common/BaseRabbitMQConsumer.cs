using InventoryService.Infrastructure.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventoryService.Infrastructure.Services.MessageConsumer.Common
{
    public abstract class BaseRabbitMQConsumer<TMessage> : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly RabbitMQSettings _settings;
        private readonly ConnectionFactory _factory;
        private IConnection _connection;
        private IChannel _channel;
        private string _queueName => typeof(TMessage).Name;

        protected BaseRabbitMQConsumer(IOptions<RabbitMQSettings> options, ILogger logger)
        {
            _settings = options.Value;
            _logger = logger;

            _factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
            };
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            var retryCount = 5;
            var delay = TimeSpan.FromSeconds(5);

            while (retryCount > 0)
            {
                try
                {
                    _connection ??= await _factory.CreateConnectionAsync();
                    _channel ??= await _connection.CreateChannelAsync();
                    await _channel.QueueDeclareAsync(_queueName, durable: false, exclusive: false, autoDelete: false);
                    break;
                }
                catch (Exception ex)
                {
                    retryCount--;
                    _logger.LogWarning(ex, "Failed to connect to RabbitMQ. Retries left: {RetryCount}", retryCount);

                    if (retryCount == 0)
                        throw;

                    await Task.Delay(delay, cancellationToken);
                }
            }

            await base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                try
                {
                    var message = JsonSerializer.Deserialize<TMessage>(json);
                    _logger.LogInformation($"Received: {json}");
                    await HandleMessageAsync(message!);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error handling message");
                }
            };

            _channel.BasicConsumeAsync(queue: _queueName, autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }

        protected abstract Task HandleMessageAsync(TMessage message);

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_channel != null) await _channel.CloseAsync();
            if (_connection != null) await _connection.CloseAsync();
            await base.StopAsync(cancellationToken);
        }
    }
}
