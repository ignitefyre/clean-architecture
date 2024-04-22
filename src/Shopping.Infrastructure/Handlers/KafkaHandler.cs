using Confluent.Kafka;

namespace Shopping.Infrastructure.Handlers;

public class KafkaHandler : IDisposable
{
    private readonly IProducer<string?, byte[]> _producer;

    public KafkaHandler()
    {
        // var config = new ProducerConfig
        // {
        //     BootstrapServers = "localhost:9092"
        // };
        //
        // _producer = new ProducerBuilder<string?, byte[]>(config)
        //     .Build();
    }
    
    public Handle Handle => _producer.Handle;

    public void Dispose()
    {
        _producer.Flush();
        _producer.Dispose();
    }
}