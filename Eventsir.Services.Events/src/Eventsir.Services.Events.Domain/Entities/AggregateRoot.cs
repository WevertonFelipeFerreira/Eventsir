using Eventsir.Services.Events.Domain.Events;
using Flunt.Notifications;

namespace Eventsir.Services.Events.Domain.Entities
{
    public abstract class AggregateRoot : Notifiable<Notification>,IEntityBase
    {
        private readonly List<IDomainEvent> _events = new List<IDomainEvent>();
        public Guid Id { get; protected set; }
        public IEnumerable<IDomainEvent> Events => _events;

        protected void AddEvent(IDomainEvent @event)
        {
            _events.Add(@event);
        }
    }
}
