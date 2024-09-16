namespace Eventsir.Services.Events.Application.UseCases.GetEventById
{
    public interface IGetEventByIdUseCase
    {
        Task<GetEventByIdOutput?> Execute(Guid id);
    }
}
