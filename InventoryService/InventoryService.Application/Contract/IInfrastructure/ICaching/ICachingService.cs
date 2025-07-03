using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Contract.IInfrastructure.ICaching
{
    public interface ICachingService
    {
        Task<T> GetDataAsync<T>(string key);
        Task SetDataAsync<T>(string key, T data);
        Task RemoveDataAsync(string key);
    }
}
