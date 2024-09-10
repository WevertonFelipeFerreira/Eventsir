using Eventsir.Services.Events.Domain.Entities;
using Eventsir.Services.Events.Domain.Events;
using Eventsir.Services.Events.Domain.Repositories;
using Eventsir.Services.Events.Domain.Repositories.UoW;
using MongoDB.Driver;

namespace Eventsir.Services.Events.Infrastructure.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IMongoCollection<Event> _collection;
        private readonly IMongoCollection<IDomainEvent> _outboxCollection;
        private readonly IUnitOfWork _unitOfWork;
        public EventRepository(IMongoDatabase database, IUnitOfWork unitOfWork)
        {
            _collection = database.GetCollection<Event>("events");
            _outboxCollection = database.GetCollection<IDomainEvent>("outbox");
            _unitOfWork = unitOfWork;
        }
        public void AddAsync(Event @event)
        {
            Action operation = () => _collection.InsertOne(_unitOfWork.Session as IClientSessionHandle, @event);
            _unitOfWork.AddOperation(operation);

            if (@event.Events.Any())
            {
                Action outboxOperation = () => _outboxCollection.InsertMany(_unitOfWork.Session as IClientSessionHandle, @event.Events);
                _unitOfWork.AddOperation(outboxOperation);
            }
        }

        public async Task<Event> GetByIdAsync(Guid id)
        {
            return await _collection.Find(c => c.Id == id).SingleOrDefaultAsync();
        }

        public void UpdateAsync(Event @event)
        {
            Action operation = () => _collection.ReplaceOne(_unitOfWork.Session as IClientSessionHandle, x => x.Id == @event.Id, @event);
            _unitOfWork.AddOperation(operation);
        }

        public void Rollback()
        {
            _unitOfWork.CleanOperations();
        }

        public async Task CommitChangesAsync()
        {
            await _unitOfWork.CommitChanges();
        }
    }
}
