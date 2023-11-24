using System.Reflection;
using System.Text.Json;
using CloudNative.CloudEvents;
using CloudNative.CloudEvents.Extensions;
using CloudNative.CloudEvents.Kafka;
using CloudNative.CloudEvents.SystemTextJson;
using Confluent.Kafka;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shopping.Application;
using Shopping.Domain;
using Shopping.Domain.Events;
using JsonProperty = Newtonsoft.Json.Serialization.JsonProperty;

namespace Shopping.Infrastructure;

public class EventPublisher : IEventPublisher
{
    private readonly IProducer<string?, byte[]> _producer;
    private const string KafkaTopic = "shopping-cart-events";

    public EventPublisher(KafkaHandler kafkaHandler)
    {
        _producer = new DependentProducerBuilder<string?, byte[]>(kafkaHandler.Handle).Build();
    }
    
    public async Task Publish(IEvent @event)
    {
        //var data = JsonConvert.SerializeObject(@event.GetData());
        
        var formatter = new JsonEventFormatter<object?>(SerializationOptions, new JsonDocumentOptions());
        
        var cloudEvent = new CloudEvent
        {
            Id = @event.Id.ToString(),
            Type = @event.Type,
            Source = new Uri(@event.Source),
            Time = DateTime.UtcNow,
            DataContentType = "application/cloudevents+json",
            Data = @event.GetData()
        };
        
        // send to message broker

        //var msg = cloudEvent.ToKafkaMessage(ContentMode.Structured);
        
        cloudEvent.SetPartitionKey(@event.Source);
        
        var kafkaMessage = cloudEvent.ToKafkaMessage(ContentMode.Structured, formatter);

        // var message = new Message<string, string>
        // {
        //     Key = @event.Id.ToString(),
        //     // Value = JsonConvert.SerializeObject(cloudEvent,
        //     //     new JsonSerializerSettings
        //     //     {
        //     //         //ContractResolver = new CloudEventContractResolver(),
        //     //         DefaultValueHandling = DefaultValueHandling.Ignore, 
        //     //         NullValueHandling = NullValueHandling.Ignore
        //     //     })
        // };
        
        await _producer.ProduceAsync(KafkaTopic, kafkaMessage);
        
        _producer.Flush();
    }
    
    private static JsonSerializerOptions SerializationOptions => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}

public class KafkaHandler : IDisposable
{
    private readonly IProducer<string?, byte[]> _producer;

    public KafkaHandler()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };
        
        _producer = new ProducerBuilder<string?, byte[]>(config)
            .Build();
    }
    
    public Handle Handle => _producer.Handle;

    public void Dispose()
    {
        _producer.Flush();
        _producer.Dispose();
    }
}

// public class CloudEventContractResolver : DefaultContractResolver
// {
//     private static readonly string[] IncludedFields = { "Id", "Type", "Source", "Time", "DataContentType", "Data" };
//
//     protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
//     {
//         var property = base.CreateProperty(member, memberSerialization);
//
//         if (!IncludedFields.Contains(property.PropertyName))
//         {
//             property.ShouldSerialize = instance => false;
//         }
//
//         return property;
//     }
// }