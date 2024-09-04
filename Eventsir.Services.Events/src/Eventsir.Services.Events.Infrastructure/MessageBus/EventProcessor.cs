using Eventsir.Services.Events.Domain.Events;
using System.Text;

namespace Eventsir.Services.Events.Infrastructure.MessageBus
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IMessageBusClient _bus;
        public EventProcessor(IMessageBusClient bus)
        {
            _bus = bus;
        }

        public void Execute(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                _bus.Publish(@event, @event.RoutingKey, "events");
            }
        }
    }
}
