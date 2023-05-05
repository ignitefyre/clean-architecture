using Shopping.Domain;

namespace Shopping.Application;

public interface IEventRepository
{
    Task Publish(IEvent @event);
}