using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductService.Application.Contract.IInfrastructure.IMessagePublisher;
using ProductService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using ProductService.Infrastructure.Settings;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Services.Outbox
{
    public sealed class OutboxPublisherService(
        IServiceScopeFactory scopeFactory,
        IOptions<OutboxSettings> outboxSettings,
        ILogger<OutboxPublisherService> logger) : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        private readonly OutboxSettings _settings = outboxSettings.Value;
        private readonly ILogger<OutboxPublisherService> _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(_settings.PollingIntervalSeconds));

            do
            {
                try
                {
                    await ProcessOutboxAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Outbox publishing cycle failed");
                }
            }
            while (await timer.WaitForNextTickAsync(stoppingToken));
        }

        private async Task ProcessOutboxAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var messagePublisher = scope.ServiceProvider.GetRequiredService<IMessagePublisher>();

            var events = await unitOfWork.EventStoreRepository.GetUnpublishedEventsAsync(
                _settings.BatchSize,
                _settings.MaxRetryCount,
                cancellationToken
            );

            if (events.Count == 0)
                return;

            foreach (var @event in events)
            {
                try
                {
                    if (!EventTypeResolver.TryResolve(@event.EventType, out var eventType) || eventType is null)
                        throw new InvalidOperationException($"Unable to resolve event type '{@event.EventType}'.");

                    var payload = JsonSerializer.Deserialize(@event.Data, eventType)
                        ?? throw new InvalidOperationException($"Unable to deserialize event '{@event.Id}'.");

                    await messagePublisher.PublishAsync(payload, eventType);

                    @event.IsPublished = true;
                    @event.PublishedOn = DateTimeOffset.UtcNow;
                    @event.LastError = null;
                }
                catch (Exception ex)
                {
                    @event.RetryCount++;
                    @event.LastError = ex.Message;

                    _logger.LogError(ex, "Failed to publish event {EventId} of type {EventType}", @event.Id, @event.EventType);
                }
            }

            await unitOfWork.SaveChangesAsync();
        }
    }
}