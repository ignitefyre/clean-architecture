using Shopping.Domain;

namespace Shopping.Application;

public interface IEventPublisher
{
    Task Publish(IEvent @event);
}