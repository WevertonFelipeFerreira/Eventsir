using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace SharedKernel.Handlers
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailService;
        private readonly IHostEnvironment _environment;
        public ExceptionHandler(IProblemDetailsService problemDetailService, IHostEnvironment environment)
        {
            _problemDetailService = problemDetailService;
            _environment = environment;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var problemDetails = new ProblemDetails
            {
                Type = "https://github.com/WevertonFelipeFerreira/Eventsir",
                Title = "An error occurred",
                Detail = exception.Message
            };

            if (_environment.IsDevelopment() || _environment.IsStaging())
            {
                problemDetails.Extensions.Add("Exception", exception);
            }

            return await _problemDetailService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problemDetails,
                Exception = exception
            });
        }
    }
}
