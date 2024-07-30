using Eventsir.Services.Events.Domain.Events;

namespace Eventsir.Services.Events.Infrastructure.MessageBus
{
    public interface IEventProcessor
    {
        void Execute(IEnumerable<IDomainEvent> events);
    }
}
