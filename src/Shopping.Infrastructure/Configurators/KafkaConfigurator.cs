using System.Reflection;
using Shopping.Domain.Events;

namespace Shopping.Infrastructure.Configurators;

public class KafkaConfigurator
{
    private int _partitionCount = 1;
    private readonly IEnumerable<Type>? _types;

    public KafkaConfigurator()
    {
        // provide event types and topic exchange names
        var assembly = Assembly.GetAssembly(typeof(IEvent));

        _types = assembly?.GetTypes()
            .Where(x => typeof(IEvent).IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false });
    }
    
    public KafkaConfigurator WithPartitionCount(int count)
    {
        // validate count
        _partitionCount = count;
        return this;
    }
    
    public void Configure()
    {
        try
        {
            // validate events and topic exchange names
        
            // configure Kafka
        }
        catch (Exception e)
        {
            // ignored
        }
    }
}