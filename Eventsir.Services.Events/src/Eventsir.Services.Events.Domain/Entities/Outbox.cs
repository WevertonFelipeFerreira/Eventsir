using Eventsir.Services.Events.Domain.Events;

namespace Eventsir.Services.Events.Domain.Entities
{
    public class Outbox
    {
        public Outbox(IDomainEvent domainEvent)
        {
            Event = domainEvent;
            IsPublished = false;
            PublishDate = null;
        }

        public IDomainEvent Event { get; private set; }
        public DateTime? PublishDate { get; set; }
        public bool IsPublished { get; private set; }

        public void Publish()
        {
            IsPublished = true;
            PublishDate = DateTime.Now;
        }
    }
}
