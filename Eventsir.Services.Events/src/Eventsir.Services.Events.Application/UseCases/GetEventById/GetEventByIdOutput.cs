using Eventsir.Services.Events.Domain.Entities;

namespace Eventsir.Services.Events.Application.UseCases.GetEventById
{
    public class GetEventByIdOutput
    {
        public GetEventByIdOutput(Guid id, string? name, DateTime? date, string? location, string? description, int? capacity, int? availableTickets, decimal? price, string? category)
        {
            Id = id;
            Name = name;
            Date = date;
            Location = location;
            Description = description;
            Capacity = capacity;
            AvailableTickets = availableTickets;
            Price = price;
            Category = category;
        }

        public Guid Id { get; set; }
        public string? Name { get; private set; }
        public DateTime? Date { get; private set; }
        public string? Location { get; private set; }
        public string? Description { get; private set; }
        public int? Capacity { get; private set; }
        public int? AvailableTickets { get; private set; }
        public decimal? Price { get; private set; }
        public string? Category { get; private set; }

        public static GetEventByIdOutput ToModel(Event entity)
        {
            return new GetEventByIdOutput(
                entity.Id,
                entity.Name,
                entity.Date,
                entity.Location,
                entity.Description,
                entity.Capacity,
                entity.AvailableTickets,
                entity.Price,
                entity.Category.ToString()
                );
        }
    }
}
