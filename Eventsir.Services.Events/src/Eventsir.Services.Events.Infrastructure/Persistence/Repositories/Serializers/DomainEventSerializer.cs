using Eventsir.Services.Events.Domain.Events;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Eventsir.Services.Events.Infrastructure.Persistence.Repositories.Serializers
{
    public class DomainEventSerializer : IBsonSerializer<IDomainEvent>
    {
        public Type ValueType => typeof(IDomainEvent);

        public IDomainEvent Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;
            var document = BsonDocument.Parse(bsonReader.ReadRawBsonDocument().ToString());

            if (document.Contains("EventType") && document["EventType"] == "EventCreated")
            {
                return BsonSerializer.Deserialize<EventCreated>(document);
            }

            throw new NotSupportedException("Unknow event.");
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            var bsonWriter = context.Writer;

            if (value is EventCreated eventCreated)
            {
                BsonSerializer.Serialize(context.Writer, eventCreated);
            }
            else
            {
                throw new NotSupportedException($"Type {value.GetType().Name} not supported.");
            }
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, IDomainEvent value)
        {
            var bsonWriter = context.Writer;

            if (value is EventCreated eventCreated)
            {
                BsonSerializer.Serialize(context.Writer, eventCreated);
            }
            else
            {
                throw new NotSupportedException($"Type {value.GetType().Name} not supported.");
            }
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;
            var document = BsonDocument.Parse(bsonReader.ReadRawBsonDocument().ToString());

            if (document.Contains("EventType") && document["EventType"] == "EventCreated")
            {
                return BsonSerializer.Deserialize<EventCreated>(document);
            }

            throw new NotSupportedException("Unknow event.");
        }
    }
}
