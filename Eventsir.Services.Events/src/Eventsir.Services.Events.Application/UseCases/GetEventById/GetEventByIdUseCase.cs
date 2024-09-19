using Eventsir.Services.Events.Domain.Repositories;
using Eventsir.Services.Events.SharedKernel.Result;

namespace Eventsir.Services.Events.Application.UseCases.GetEventById
{
    public class GetEventByIdUseCase : IGetEventByIdUseCase
    {
        private readonly IEventRepository _eventRepository;
        public GetEventByIdUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Result<GetEventByIdOutput>> Execute(Guid id)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);
            if (eventEntity is null)
                return Result<GetEventByIdOutput>.CreateError("Event not found", EResultType.NotFound);

            return Result<GetEventByIdOutput>.CreateSuccess(GetEventByIdOutput.ToModel(eventEntity));
        }
    }
}
