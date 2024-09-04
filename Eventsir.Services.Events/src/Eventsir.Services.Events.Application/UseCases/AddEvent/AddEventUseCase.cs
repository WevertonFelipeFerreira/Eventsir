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
            try
            {
                _eventRepository.AddAsync(entity);
                await _eventRepository.CommitChangesAsync();

                return new AddEventOutput(entity.Id);
            }
            catch(Exception ex)
            {
                _eventRepository.Rollback();

                Console.WriteLine(ex.Message);
                return null!;
            }
        }
    }
}
