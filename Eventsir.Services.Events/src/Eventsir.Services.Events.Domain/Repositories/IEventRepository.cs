using Eventsir.Services.Events.Domain.Entities;

namespace Eventsir.Services.Events.Domain.Repositories
{
    public interface IEventRepository
    {
        Task<Event> GetByIdAsync(Guid id);
        void AddAsync(Event @event);
        void UpdateAsync(Event @event);
        void Rollback();
        Task CommitChangesAsync();
    }
}