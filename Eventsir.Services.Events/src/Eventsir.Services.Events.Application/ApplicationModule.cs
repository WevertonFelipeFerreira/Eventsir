using Eventsir.Services.Events.Application.UseCases.AddEvent;
using Eventsir.Services.Events.Application.UseCases.GetEventById;
using Microsoft.Extensions.DependencyInjection;

namespace Eventsir.Services.Events.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddUseCases();

            return services;
        }

        private static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IAddEventUseCase, AddEventUseCase>();
            services.AddScoped<IGetEventByIdUseCase, GetEventByIdUseCase>();

            return services;
        }
    }
}
