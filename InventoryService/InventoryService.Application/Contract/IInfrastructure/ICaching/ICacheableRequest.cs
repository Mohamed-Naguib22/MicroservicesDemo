using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Application.Contract.IInfrastructure.ICaching
{
    public interface ICacheableRequest
    {
        string CacheKey { get; }
        TimeSpan Expiry { get; }
    }
}
