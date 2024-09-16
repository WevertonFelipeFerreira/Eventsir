using Eventsir.Services.Events.Application.UseCases.AddEvent;
using Eventsir.Services.Events.Application.UseCases.GetEventById;
using Microsoft.AspNetCore.Mvc;

namespace Eventsir.Services.Events.API.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddEvent([FromServices] IAddEventUseCase useCase, [FromBody] AddEventInput input)
        {
            var output = await useCase.Execute(input);
            if (output is null)
            {
                return UnprocessableEntity();
            }

            return Created(nameof(AddEvent), output);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById([FromServices] IGetEventByIdUseCase useCase, Guid id)
        {
            var output = await useCase.Execute(id);
            if (output is null)
            {
                return NotFound();
            }

            return Ok(output);
        }
    }
}
