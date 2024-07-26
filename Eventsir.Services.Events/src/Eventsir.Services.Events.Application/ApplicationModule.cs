using Eventsir.Services.Events.Application.UseCases.AddEvent;
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

            return services;
        }
    }
}
