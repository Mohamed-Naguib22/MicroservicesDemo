using InventoryService.Application.Contract.IInfrastructure.ICaching;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InventoryService.Persistence.Caching
{
    public sealed class RedisCacheService(IConnectionMultiplexer redis) : ICachingService
    {
        private readonly IDatabase _database = redis.GetDatabase();
        private static readonly JsonSerializerOptions _options = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            PropertyNameCaseInsensitive = true
        };

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.IsNullOrEmpty)
                return default;

            var type = typeof(T);

            if (type.IsInterface && type.IsGenericType)
            {
                var listType = typeof(List<>).MakeGenericType(type.GetGenericArguments());
                var result = JsonSerializer.Deserialize((string)value!, listType, _options);
                return result == null ? default : (T)result;
            }

            return JsonSerializer.Deserialize<T>((string)value!, _options);
        }

        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var serialized = JsonSerializer.Serialize(value, _options);

            return await _database.StringSetAsync(key, serialized, expiry);
        }

        public async Task<bool> DeleteAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<long> DeleteByPatternAsync(string pattern)
        {
            var server = redis.GetServers().First();
            var keys = server.Keys(pattern: $"*{pattern}*").ToArray();

            if (keys.Length == 0)
                return 0;

            return await _database.KeyDeleteAsync(keys);
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public async Task<bool> TryGetValueAsync<T>(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public async Task<T> FetchOrCacheAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiry = null)
        {
            var cached = await GetAsync<T>(key);
            if (cached != null && cached is not ICollection { Count: 0 })
            {
                return cached;
            }
            var value = await factory();
            await SetAsync(key, value, expiry);
            return value;
        }

        public async Task<bool> ExpireAsync(string key, TimeSpan expiry)
        {
            return await _database.KeyExpireAsync(key, expiry);
        }

        public T? Get<T>(string key)
        {
            return GetAsync<T>(key).GetAwaiter().GetResult();
        }

        public void Set<T>(string key, T value, TimeSpan? expiry = null)
        {
            SetAsync(key, value, expiry).GetAwaiter().GetResult();
        }

        public void Remove(string key)
        {
            DeleteAsync(key).GetAwaiter().GetResult();
        }

        public bool Exists(string key)
        {
            return ExistsAsync(key).GetAwaiter().GetResult();
        }

        public bool TryGetValue<T>(string key, out T? value)
        {
            value = Get<T>(key);
            return value != null;
        }

        public async Task<long> ListPushAsync<T>(string key, T value)
        {
            var serialized = JsonSerializer.Serialize(value);
            return await _database.ListRightPushAsync(key, serialized);
        }

        public async Task<long> ListLengthAsync(string key)
        {
            return await _database.ListLengthAsync(key);
        }

        public async Task<List<T>> ListRangeAsync<T>(string key, long start = 0, long stop = -1)
        {
            var values = await _database.ListRangeAsync(key, start, stop);
            return values
                .Select(v => JsonSerializer.Deserialize<T>(v.ToString()))
                .Where(item => item != null)
                .ToList()!;
        }

        public async Task<bool> ListTrimAsync(string key, long start, long stop)
        {
            await _database.ListTrimAsync(key, start, stop);
            return true;
        }
    }
}
