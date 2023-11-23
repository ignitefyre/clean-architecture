using System.Reflection;
using CloudNative.CloudEvents;
using Confluent.Kafka;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shopping.Application;
using Shopping.Domain;

namespace Shopping.Infrastructure;

public class EventPublisher : IEventPublisher
{
    private readonly IProducer<string,string> _producer;
    private const string KafkaTopic = "shopping-cart-events";

    public EventPublisher(KafkaHandler kafkaHandler)
    {
        _producer = new DependentProducerBuilder<string, string>(kafkaHandler.Handle).Build();
    }
    
    public async Task Publish(IEvent @event)
    {
        var cloudEvent = new CloudEvent
        {
            Id = @event.Id.ToString(),
            Type = @event.Type,
            Source = new Uri(@event.Source),
            Time = DateTime.UtcNow,
            DataContentType = "application/json",
            Data = @event.GetData()
        };
        
        // send to message broker

        var message = new Message<string, string>
        {
            Key = @event.Id.ToString(),
            Value = JsonConvert.SerializeObject(cloudEvent,
                new JsonSerializerSettings
                {
                    ContractResolver = new CloudEventContractResolver(),
                    DefaultValueHandling = DefaultValueHandling.Ignore, 
                    NullValueHandling = NullValueHandling.Ignore
                })
        };
        
        await _producer.ProduceAsync(KafkaTopic, message);
        
        _producer.Flush();
    }
}

public class KafkaHandler : IDisposable
{
    private readonly IProducer<string, string> _producer;

    public KafkaHandler()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };
        
        var builder = new ProducerBuilder<string, string>(config);
        _producer = builder.Build();
    }
    
    public Handle Handle => _producer.Handle;

    public void Dispose()
    {
        _producer.Flush();
        _producer.Dispose();
    }
}

public class CloudEventContractResolver : DefaultContractResolver
{
    private static readonly string[] IncludedFields = { "Id", "Type", "Source", "Time", "DataContentType", "Data" };

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);

        if (!IncludedFields.Contains(property.PropertyName))
        {
            property.ShouldSerialize = instance => false;
        }

        return property;
    }
}