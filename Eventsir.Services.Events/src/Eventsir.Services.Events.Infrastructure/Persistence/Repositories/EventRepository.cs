using Eventsir.Services.Events.Domain.Entities;
using Eventsir.Services.Events.Domain.Repositories;
using MongoDB.Driver;

namespace Eventsir.Services.Events.Infrastructure.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IMongoCollection<Event> _collection;
        public EventRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Event>("events");
        }
        public async Task AddAsync(Event @event)
        {
            await _collection.InsertOneAsync(@event);
        }

        public async Task<Event> GetByIdAsync(Guid id)
        {
            return await _collection.Find(c => c.Id == id).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(Event @event)
        {
            await _collection.ReplaceOneAsync(o => o.Id == @event.Id, @event);
        }
    }
}
