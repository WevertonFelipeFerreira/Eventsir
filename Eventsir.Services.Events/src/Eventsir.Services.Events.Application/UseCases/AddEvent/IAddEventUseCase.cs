using Eventsir.Services.Events.SharedKernel.Result;

namespace Eventsir.Services.Events.Application.UseCases.AddEvent
{
    public interface IAddEventUseCase
    {
        Task<Result<AddEventOutput>> Execute(AddEventInput input);
    }
}
