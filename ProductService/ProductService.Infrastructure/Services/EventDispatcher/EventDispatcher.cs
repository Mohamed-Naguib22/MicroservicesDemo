using ProductService.Application.Contract.IInfrastructure.IEventDispatcher;
using ProductService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using ProductService.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Services.EventDispatcher
{
    public sealed class EventDispatcher(IUnitOfWork unitOfWork) : IEventDispatcher
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task AppendEventAsync<T>(T @event)
        {
            var eventType = typeof(T).Name;
            var jsonData = JsonSerializer.Serialize(@event);

            var eventEntity = new EventEntity
            {
                Id = Guid.NewGuid(),
                EventType = eventType,
                Data = jsonData,
                OccurredOn = DateTimeOffset.Now,
                IsPublished = false,
            };

            await _unitOfWork.EventStoreRepository.StoreEventAsync(eventEntity);
        }
    }
}