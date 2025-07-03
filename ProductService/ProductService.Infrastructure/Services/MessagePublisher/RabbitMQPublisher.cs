using Microsoft.Extensions.Options;
using ProductService.Application.Contract.IInfrastructure.IMessagePublisher;
using ProductService.Infrastructure.Settings;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Services.MessagePublisher
{
    public class RabbitMQPublisher : IMessagePublisher, IAsyncDisposable
    {
        private readonly ConnectionFactory _factory;
        private readonly RabbitMQSettings _rabbitMQSettings;
        private IConnection _connection;
        private IChannel _channel;
        
        public RabbitMQPublisher(IOptions<RabbitMQSettings> rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings.Value;

            _factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
            };
        }

        public async Task PublishAsync<T>(T message)
        {
            _connection ??= await _factory.CreateConnectionAsync();
            _channel ??= await _connection.CreateChannelAsync();

            var queueName = typeof(T).Name;
            await _channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            await _channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body);
        }

        public async ValueTask DisposeAsync()
        {
            if (_channel != null) await _channel.CloseAsync();
            if (_connection != null) await _connection.CloseAsync();
        }
    }
}
