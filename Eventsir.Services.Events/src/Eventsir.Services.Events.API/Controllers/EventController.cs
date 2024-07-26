using Eventsir.Services.Events.Application.UseCases.AddEvent;
using Microsoft.AspNetCore.Mvc;

namespace Eventsir.Services.Events.API.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromServices] IAddEventUseCase useCase, [FromBody] AddEventInput input)
        {
            var output = await useCase.Execute(input);
            return Created(output);
        }
    }
}
