using Eventsir.Services.Events.Domain.Repositories;
using Eventsir.Services.Events.Domain.Repositories.UoW;
using Eventsir.Services.Events.Infrastructure.MessageBus;
using Eventsir.Services.Events.Infrastructure.Persistence;
using Eventsir.Services.Events.Infrastructure.Persistence.Repositories;
using Eventsir.Services.Events.Infrastructure.Persistence.Repositories.Serializers;
using Eventsir.Services.Events.Infrastructure.Persistence.Repositories.UoW;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using RabbitMQ.Client;

namespace Eventsir.Services.Events.Infrastructure
{
    public static class InfraModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddMongo()
                .AddRepositories()
                .AddRabbitMq();

            return services;
        }

        private static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();
                var options = new MongoDbOptions();

                configuration?.GetSection("Mongo").Bind(options);

                return options;
            });

            services.AddSingleton<IMongoClient>(sp =>
            {
                BsonSerializer.RegisterSerializer(new DomainEventSerializer());

                var options = sp.GetService<MongoDbOptions>();
                return new MongoClient(options?.ConnectionString);
            });

            services.AddTransient(sp =>
            {
                var options = sp.GetService<MongoDbOptions>();
                var mongoClient = sp.GetService<IMongoClient>();

                return mongoClient!.GetDatabase(options?.Database, new MongoDatabaseSettings { GuidRepresentation = GuidRepresentation.Standard});
            });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEventRepository, EventRepository>();

            return services;
        }

        private static IServiceCollection AddRabbitMq(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();

                var options = new RabbitMQOptions();

                configuration.GetSection("RabbitMQ").Bind(options);

                Console.WriteLine(options.Hostname);

                var connectionFactory = new ConnectionFactory
                {
                    HostName = options.Hostname
                };

                var connection = connectionFactory.CreateConnection("event-service-producer");
                var producerConnection = new ProducerConnection(connection);

                producerConnection.Connection.CreateModel();

                return producerConnection;
            });

            services.AddScoped<IMessageBusClient, RabbitMQClient>();
            services.AddTransient<IEventProcessor, EventProcessor>();

            return services;
        }
    }
}
