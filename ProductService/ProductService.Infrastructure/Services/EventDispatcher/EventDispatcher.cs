using ProductService.Application.Contract.IInfrastructure.IEventDispatcher;
using ProductService.Application.Contract.IInfrastructure.IMessagePublisher;
using ProductService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using ProductService.Domain.Events.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Services.EventDispatcher
{
    public sealed class EventDispatcher(IUnitOfWork unitOfWork, IMessagePublisher publisher) : IEventDispatcher
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMessagePublisher _publisher = publisher;

        public async Task AppendAndPublishEventAsync<T>(T @event)
        {
            var eventType = typeof(T).Name;
            var jsonData = JsonSerializer.Serialize(@event);

            var eventEntity = new EventEntity
            {
                Id = Guid.NewGuid(),
                EventType = eventType,
                Data = jsonData,
                OccurredOn = DateTimeOffset.Now,
            };

            await _unitOfWork.EventStoreRepository.StoreEventAsync(eventEntity);

            await _publisher.PublishAsync(@event);
        }
    }
}
