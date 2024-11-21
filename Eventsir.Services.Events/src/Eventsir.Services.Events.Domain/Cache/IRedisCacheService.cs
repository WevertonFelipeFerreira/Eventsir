using Eventsir.Services.Events.Domain.Entities;

namespace Eventsir.Services.Events.Domain.Cache
{
    public interface IRedisCacheService
    {
        Task<T?> GetAsync<T>(string key)
            where T : AggregateRoot;
        Task SetAsync<T>(string key, T value)
            where T : AggregateRoot;
        Task RemoveAsync(string key);
    }
}
