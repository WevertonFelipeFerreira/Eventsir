using Eventsir.Services.Events.Domain.Entities;
using Eventsir.Services.Events.Domain.Enums;

namespace Eventsir.Services.Events.Application.UseCases.AddEvent
{
    public class AddEventInput
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public DateTime? Date { get; set; }
        public int? Capacity { get; set; }
        public int? AvailableTickets { get; set; }
        public decimal? Price { get; set; }

        public Event ToEntity()
        {
            var success = Enum.TryParse(Category?.ToUpper(), out ECategory myStatus);

            return new Event(
                Name,
                Date,
                Location,
                Description,
                Capacity,
                AvailableTickets,
                Price,
                success ? myStatus : ECategory.NONE);
        }
    }
}
