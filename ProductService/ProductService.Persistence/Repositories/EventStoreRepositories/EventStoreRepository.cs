using Microsoft.EntityFrameworkCore;
using ProductService.Application.Contract.IInfrastructure.IRepositories.IEventStoreRepositories;
using ProductService.Domain.Entities.Common;
using ProductService.Persistence.Context;
using ProductService.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.Persistence.Repositories.EventStoreRepositories
{
    public sealed class EventStoreRepository(EventStoreDbContext context) : BaseRepository<EventEntity>(context), IEventStoreRepository
    {
        public async Task StoreEventAsync(EventEntity @event)
        {
            await AddAsync(@event);
            await _context.SaveChangesAsync();
        }

        public async Task<List<EventEntity>> GetUnpublishedEventsAsync(int batchSize, int maxRetryCount, CancellationToken cancellationToken = default)
        {
            return await _context.Events
                .Where(e => !e.IsPublished && e.RetryCount < maxRetryCount)
                .OrderBy(e => e.OccurredOn)
                .Take(batchSize)
                .ToListAsync(cancellationToken);
        }
    }
}