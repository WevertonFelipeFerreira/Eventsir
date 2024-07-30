using RabbitMQ.Client;

namespace Eventsir.Services.Events.Infrastructure.MessageBus
{
    public class ProducerConnection
    {
        public ProducerConnection(IConnection connection)
        {
            Connection = connection;
        }
        public IConnection Connection { get; private set; }
    }
}
