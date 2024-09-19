using Eventsir.Services.Events.SharedKernel.Result;

namespace Eventsir.Services.Events.Application.UseCases.GetEventById
{
    public interface IGetEventByIdUseCase
    {
        Task<Result<GetEventByIdOutput>> Execute(Guid id);
    }
}
