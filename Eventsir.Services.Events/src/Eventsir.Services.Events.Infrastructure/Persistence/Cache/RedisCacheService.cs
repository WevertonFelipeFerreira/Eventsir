using Eventsir.Services.Events.Domain.Cache;
using Eventsir.Services.Events.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Eventsir.Services.Events.Infrastructure.Persistence.Cache
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;
        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetAsync<T>(string key)
            where T : AggregateRoot
        {
            var bytes = await _cache.GetAsync(key);
            if (bytes == null)
                return null;

            return JsonSerializer.Deserialize<T>(bytes);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task SetAsync<T>(string key, T value)
            where T : AggregateRoot
        {
            var serializedObject = JsonSerializer.Serialize(value);

            var objectBytes = Encoding.UTF8.GetBytes(serializedObject);

            var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            await _cache.SetAsync(key, objectBytes, options);
        }
    }
}
