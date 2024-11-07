using Eventsir.Services.Events.Domain.Utils;

namespace Eventsir.Services.Events.Domain.Events
{
    public class EventCreated : IDomainEvent
    {
        public EventCreated(Guid id, string? name, DateTime? date, string? location, string? description)
        {
            Id = id;
            Name = name;
            Date = date;
            Location = location;
            Description = description;
            RoutingKey = this.ToDashCase();
            Published = false;
        }

        public Guid Id { get; set; }
        public string? Name { get; private set; }
        public DateTime? Date { get; private set; }
        public string? Location { get; private set; }
        public string? Description { get; private set; }
        public string RoutingKey { get; private set; }
        public bool Published { get; private set; }
    }
}
