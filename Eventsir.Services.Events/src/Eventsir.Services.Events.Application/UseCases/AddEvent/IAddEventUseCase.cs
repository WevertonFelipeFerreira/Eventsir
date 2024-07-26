namespace Eventsir.Services.Events.Application.UseCases.AddEvent
{
    public interface IAddEventUseCase
    {
        Task<AddEventOutput> Execute(AddEventInput input);
    }
}
