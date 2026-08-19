using ProductService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using ProductService.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.Application.Contract.IInfrastructure.IRepositories.IEventStoreRepositories
{
    public interface IEventStoreRepository : IBaseRepository<EventEntity>
    {
        Task StoreEventAsync(EventEntity @event);
        Task<List<EventEntity>> GetUnpublishedEventsAsync(int batchSize, int maxRetryCount, CancellationToken cancellationToken = default);
    }
}