namespace Eventsir.Services.Events.Application.UseCases.AddEvent
{
    public class AddEventOutput
    {
        public AddEventOutput(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
