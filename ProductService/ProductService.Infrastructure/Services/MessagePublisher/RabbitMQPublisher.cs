using Microsoft.Extensions.Options;
using ProductService.Application.Contract.IInfrastructure.IMessagePublisher;
using ProductService.Infrastructure.Settings;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Services.MessagePublisher
{
    public sealed class RabbitMQPublisher : IMessagePublisher, IAsyncDisposable
    {
        private readonly ConnectionFactory _factory;
        private readonly RabbitMQSettings _rabbitMQSettings;

        private IConnection? _connection;
        private IChannel? _channel;

        private readonly SemaphoreSlim _connectionLock = new(1, 1);

        public RabbitMQPublisher(IOptions<RabbitMQSettings> rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings.Value;

            _factory = new ConnectionFactory
            {
                HostName = _rabbitMQSettings.HostName,
                UserName = _rabbitMQSettings.UserName,
                Password = _rabbitMQSettings.Password,
                VirtualHost = _rabbitMQSettings.VirtualHost
            };
        }

        public Task PublishAsync<T>(T message)
        {
            return PublishInternalAsync(
                queueName: typeof(T).Name,
                json: JsonSerializer.Serialize(message)
            );
        }

        public Task PublishAsync(object message, Type messageType)
        {
            return PublishInternalAsync(
                queueName: messageType.Name,
                json: JsonSerializer.Serialize(message, messageType)
            );
        }

        private async Task PublishInternalAsync(string queueName, string json)
        {
            var channel = await EnsureChannelAsync();

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false);

            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: queueName,
                body: body
            );
        }

        private async Task<IChannel> EnsureChannelAsync()
        {
            if (_channel is { IsOpen: true })
                return _channel;

            await _connectionLock.WaitAsync();

            try
            {
                if (_channel is { IsOpen: true })
                    return _channel;

                if (_connection is not { IsOpen: true })
                {
                    _connection = await _factory.CreateConnectionAsync();
                }

                _channel = await _connection.CreateChannelAsync();

                return _channel;
            }
            finally
            {
                _connectionLock.Release();
            }
        }

        public async ValueTask DisposeAsync()
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

            _connectionLock.Dispose();
        }
    }
}