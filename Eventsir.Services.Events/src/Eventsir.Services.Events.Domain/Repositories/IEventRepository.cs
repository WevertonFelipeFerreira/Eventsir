using Eventsir.Services.Events.Domain.Entities;

namespace Eventsir.Services.Events.Domain.Repositories
{
    public interface IEventRepository
    {
        Task<Event> GetByIdAsync(Guid id);
        Task AddAsync(Event @event);
        Task UpdateAsync(Event @event);
    }
}