using System.Text.Json;
using CloudNative.CloudEvents;
using CloudNative.CloudEvents.Extensions;
using CloudNative.CloudEvents.Kafka;
using CloudNative.CloudEvents.SystemTextJson;
using Confluent.Kafka;
using Shopping.Application;
using Shopping.Domain;
using Shopping.Infrastructure.Handlers;

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
        var formatter = new JsonEventFormatter<object?>(SerializationOptions, new JsonDocumentOptions());
        
        var cloudEvent = new CloudEvent
        {
            Id = @event.Id.ToString(),
            Type = @event.Type,
            Source = new Uri(@event.Source),
            Time = DateTime.UtcNow,
            DataContentType = "application/json",
            Data = @event.GetData()
        };
        
        cloudEvent.SetPartitionKey(@event.Source);
        
        var kafkaMessage = cloudEvent.ToKafkaMessage(ContentMode.Structured, formatter);
        
        await _producer.ProduceAsync(KafkaTopic, kafkaMessage);
        
        _producer.Flush();
    }
    
    private static JsonSerializerOptions SerializationOptions => new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
}