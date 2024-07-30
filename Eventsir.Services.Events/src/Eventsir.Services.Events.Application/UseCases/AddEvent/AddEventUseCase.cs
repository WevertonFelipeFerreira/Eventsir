using Eventsir.Services.Events.Domain.Repositories;
using Eventsir.Services.Events.Infrastructure.MessageBus;

namespace Eventsir.Services.Events.Application.UseCases.AddEvent
{
    public class AddEventUseCase : IAddEventUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventProcessor _eventProcessor;
        public AddEventUseCase(IEventRepository eventRepository, IEventProcessor eventProcessor)
        {
            _eventRepository = eventRepository;
            _eventProcessor = eventProcessor;
        }
        public async Task<AddEventOutput> Execute(AddEventInput input)
        {
            var entity = input.ToEntity();

            await _eventRepository.AddAsync(entity);
            _eventProcessor.Execute(entity.Events);

            return new AddEventOutput(entity.Id);
        }
    }
}
