
using Eventsir.Services.Events.Domain.Repositories;

namespace Eventsir.Services.Events.Application.UseCases.AddEvent
{
    public class AddEventUseCase : IAddEventUseCase
    {
        private readonly IEventRepository _eventRepository;
        public AddEventUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task<AddEventOutput> Execute(AddEventInput input)
        {
            var entity = input.ToEntity();
            await _eventRepository.AddAsync(entity);
            return new AddEventOutput(entity.Id);
        }
    }
}
