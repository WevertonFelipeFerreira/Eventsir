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
            var problemDetailsBuilder = new ProblemDetailsBuilder(httpContext)
                .SetDefaults()
                .SetStatusCode(500)
                .SetDetail(exception.Message);

            if (_environment.IsDevelopment() || _environment.IsStaging())
            {
                problemDetailsBuilder.WithException(exception);
            }

            return await _problemDetailService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problemDetailsBuilder.Build(),
                Exception = exception
            });
        }
    }
}
