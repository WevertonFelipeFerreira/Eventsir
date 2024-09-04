namespace Eventsir.Services.Events.Domain.Events
{
    public interface IDomainEvent
    {
        string RoutingKey { get; }
        bool Published { get; }
    }
}
