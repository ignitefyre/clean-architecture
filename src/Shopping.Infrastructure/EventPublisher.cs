using System.Text.Json;
using CloudNative.CloudEvents;
using CloudNative.CloudEvents.Kafka;
using CloudNative.CloudEvents.SystemTextJson;
using Confluent.Kafka;
using Shopping.Application;
using Shopping.Domain;
using Shopping.Domain.Events;
using Shopping.Infrastructure.Builders;
using Shopping.Infrastructure.Handlers;

namespace Shopping.Infrastructure;

public class EventPublisher : IEventPublisher
{
    private readonly IProducer<string?, byte[]> _producer;

    public EventPublisher(KafkaHandler kafkaHandler)
    {
        //_producer = new DependentProducerBuilder<string?, byte[]>(kafkaHandler.Handle).Build();
    }
    
    public async Task Publish(IEvent @event)
    {
        return;
        
        // var ce = new CloudEventBuilder(@event.Id, @event.Type, @event.Source)
        //     .WithPartitionKey(@event.Source)
        //     .WithTime(DateTime.UtcNow)
        //     .WithData(@event.GetData())
        //     .Build();
        //
        // var formatter = new JsonEventFormatter<object?>(
        //     new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }, 
        //     new JsonDocumentOptions());
        //
        // var kafkaMessage = ce.ToKafkaMessage(ContentMode.Structured, formatter);
        //
        // await _producer.ProduceAsync(@event.GetTopicName(), kafkaMessage);
    }
}