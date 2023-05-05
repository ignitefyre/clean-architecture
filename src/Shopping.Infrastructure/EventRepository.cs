using CloudNative.CloudEvents;
using Shopping.Application;
using Shopping.Domain;

namespace Shopping.Infrastructure;

public class EventRepository : IEventRepository
{
    public async Task Publish(IEvent @event)
    {
        var cloudEvent = new CloudEvent(CloudEventsSpecVersion.V1_0)
        {
            Id = @event.Id.ToString(),
            Type = @event.Type,
            Source = new Uri(@event.Source),
            Time = DateTime.UtcNow,
            DataContentType = "application/json",
            Data = @event.GetData()
        };
        
        // send to message broker
    }
}