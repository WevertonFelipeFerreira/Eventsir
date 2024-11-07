using Eventsir.Services.Events.Application;
using Eventsir.Services.Events.Infrastructure;
using SharedKernel.Handlers;
//using Hellang.Middleware.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddInfrastructure()
    .AddApplication();

builder.Services
    .AddProblemDetails(options =>
        options.CustomizeProblemDetails = ctx =>
        {
            ctx.ProblemDetails.Extensions.Add("trace-id", ctx.HttpContext.TraceIdentifier);
            ctx.ProblemDetails.Extensions.Add("instance", (string)ctx.HttpContext.Request.Path);
        }
        );

builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseStatusCodePages();

app.UseAuthorization();

app.MapControllers();

app.Run();

