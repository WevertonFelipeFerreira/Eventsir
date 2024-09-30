using Eventsir.Services.Events.Application.UseCases.AddEvent;
using Eventsir.Services.Events.Application.UseCases.GetEventById;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Result;

namespace Eventsir.Services.Events.API.Controllers
{
    [ApiController]
    [Route("api/events")]
    //TODO Create a new controller base to standardize all errors in problem details
    public class EventController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddEvent([FromServices] IAddEventUseCase useCase, [FromBody] AddEventInput input)
        {
            var result = await useCase.Execute(input);

            if (result.ResultType == EResultType.Unprocessable)
            {
                return UnprocessableEntity(new { ErrorMessage = result.ErrorMessage ?? "Unprocessable entity" });
            }

            return Created(nameof(AddEvent), result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById([FromServices] IGetEventByIdUseCase useCase, Guid id)
        {
            var result = await useCase.Execute(id);

            if (result.ResultType == EResultType.NotFound)
            {
                return NotFound(new { ErrorMessage = result.ErrorMessage ?? "Not found" });
            }

            return Ok(result.Value);
        }
    }
}
