using Eventsir.Services.Events.Domain.Enums;
using Eventsir.Services.Events.Domain.Events;

namespace Eventsir.Services.Events.Domain.Entities
{
    public class Event : AggregateRoot
    {
        public Event(string name, DateTime date, string location, string description, int capacity, int availableTickets, decimal price, ECategory category)
        {
            Id = Guid.NewGuid();
            Name = name;
            Date = date;
            Location = location;
            Description = description;
            Capacity = capacity;
            AvailableTickets = availableTickets;
            Price = price;
            Category = category;

            AddEvent(new EventCreated(Id, name, date, location, description));
        }

        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public string Location { get; private set; }
        public string Description { get; private set; }
        public int Capacity { get; private set; }
        public int AvailableTickets { get; private set; }
        public decimal Price { get; private set; }
        public ECategory Category { get; private set; }
    }
}
