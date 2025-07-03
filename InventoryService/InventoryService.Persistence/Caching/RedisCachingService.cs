using InventoryService.Application.Contract.IInfrastructure.ICaching;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventoryService.Persistence.Caching
{
    public class RedisCacheService(IDistributedCache cache) : ICachingService
    {
        private readonly IDistributedCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));

        public async Task<T> GetDataAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Cache key cannot be null or empty.", nameof(key));

            var data = await _cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(data))
                return default;

            try
            {
                return JsonSerializer.Deserialize<T>(data);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to deserialize cached data.", ex);
            }
        }

        public async Task SetDataAsync<T>(string key, T data)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Cache key cannot be null or empty.", nameof(key));

            if (data == null)
                throw new ArgumentNullException(nameof(data), "Cannot cache null data.");

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            try
            {
                var serializedData = JsonSerializer.Serialize(data);
                await _cache.SetStringAsync(key, serializedData, options);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to serialize and cache data.", ex);
            }
        }
    }
}
