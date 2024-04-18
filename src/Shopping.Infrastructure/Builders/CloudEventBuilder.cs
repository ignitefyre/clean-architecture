using CloudNative.CloudEvents;
using CloudNative.CloudEvents.Extensions;

namespace Shopping.Infrastructure.Builders;

public class CloudEventBuilder
{
    private readonly CloudEvent _cloudEvent;

    public CloudEventBuilder(Guid id, string type, string source)
    {
        _cloudEvent = new CloudEvent
        {
            Id = id.ToString(),
            Type = type,
            Source = new Uri(source)
        };
    }
    
    public CloudEventBuilder WithData<T>(T data, string contentType = "application/json")
    {
        _cloudEvent.DataContentType = contentType;
        _cloudEvent.Data = data;
        return this;
    }

    public CloudEventBuilder WithSubject(string subject)
    {
        _cloudEvent.Subject = subject;
        return this;
    }

    public CloudEventBuilder WithTime(DateTimeOffset time)
    {
        _cloudEvent.Time = time;
        return this;
    }
    
    public CloudEventBuilder WithPartitionKey(string key)
    {
        _cloudEvent.SetPartitionKey(key);
        return this;
    }

    public CloudEvent Build()
    {
        // Validate CloudEvent or perform additional checks before returning
        return _cloudEvent;
    }
}