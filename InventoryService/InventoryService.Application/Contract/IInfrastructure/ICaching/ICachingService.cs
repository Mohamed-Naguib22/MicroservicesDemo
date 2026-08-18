using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Contract.IInfrastructure.ICaching
{
    public interface ICachingService
    {
        Task<T?> GetAsync<T>(string key);
        T? Get<T>(string key);
        Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null);
        void Set<T>(string key, T value, TimeSpan? expiry = null);
        Task<bool> DeleteAsync(string key);
        Task<long> DeleteByPatternAsync(string pattern);
        void Remove(string key);
        Task<bool> ExistsAsync(string key);
        bool Exists(string key);
        Task<bool> TryGetValueAsync<T>(string key);
        bool TryGetValue<T>(string key, out T? value);
        Task<T> FetchOrCacheAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiry = null);
        Task<bool> ExpireAsync(string key, TimeSpan expiry);
        Task<long> ListPushAsync<T>(string key, T value);
        Task<long> ListLengthAsync(string key);
        Task<List<T>> ListRangeAsync<T>(string key, long start = 0, long stop = -1);
        Task<bool> ListTrimAsync(string key, long start, long stop);
    }
}
