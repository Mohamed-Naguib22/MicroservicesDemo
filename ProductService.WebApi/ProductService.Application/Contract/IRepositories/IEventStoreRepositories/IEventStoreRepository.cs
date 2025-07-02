using ProductService.Domain.Events.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Contract.IRepositories.IEventStoreRepositories
{
    public interface IEventStoreRepository
    {
        Task StoreEventAsync(EventEntity @event);
    }
}
