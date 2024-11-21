using Eventsir.Services.Events.Domain.Cache;
using Eventsir.Services.Events.Domain.Entities;
using Eventsir.Services.Events.Domain.Repositories;
using SharedKernel.Result;

namespace Eventsir.Services.Events.Application.UseCases.GetEventById
{
    public class GetEventByIdUseCase : IGetEventByIdUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IRedisCacheService _cacheService;
        public GetEventByIdUseCase(IEventRepository eventRepository, IRedisCacheService cacheService)
        {
            _eventRepository = eventRepository;
            _cacheService = cacheService;
        }

        public async Task<Result<GetEventByIdOutput>> Execute(Guid id)
        {
            #region CACHE VERIFICATION
            var cacheEntity = await _cacheService.GetAsync<Event>($"events:{id}");
            if (cacheEntity is not null)
                return Result<GetEventByIdOutput>.CreateSuccess(GetEventByIdOutput.ToModel(cacheEntity));
            #endregion

            var eventEntity = await _eventRepository.GetByIdAsync(id);
            if (eventEntity is null)
                return Result<GetEventByIdOutput>.CreateError("Event not found", EResultType.NotFound);

            await _cacheService.SetAsync($"events:{eventEntity.Id}", eventEntity);

            return Result<GetEventByIdOutput>.CreateSuccess(GetEventByIdOutput.ToModel(eventEntity));
        }
    }
}
