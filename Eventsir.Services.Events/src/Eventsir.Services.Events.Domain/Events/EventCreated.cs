namespace Eventsir.Services.Events.Domain.Events
{
    public class EventCreated : IDomainEvent
    {
        public EventCreated(Guid id, string? name, DateTime date, string? location, string? description)
        {
            Id = id;
            Name = name;
            Date = date;
            Location = location;
            Description = description;
        }

        public Guid Id { get; set; }
        public string? Name { get; private set; }
        public DateTime Date { get; private set; }
        public string? Location { get; private set; }
        public string? Description { get; private set; }
    }
}
