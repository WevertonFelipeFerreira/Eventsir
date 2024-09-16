using Eventsir.Services.Events.Domain.Repositories;

namespace Eventsir.Services.Events.Application.UseCases.GetEventById
{
    public class GetEventByIdUseCase : IGetEventByIdUseCase
    {
        private readonly IEventRepository _eventRepository;
        public GetEventByIdUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<GetEventByIdOutput?> Execute(Guid id)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);
            if (eventEntity is null)
                return null;

            return GetEventByIdOutput.ToModel(eventEntity);
        }
    }
}
