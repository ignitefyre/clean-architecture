using Shopping.Domain;
using Shopping.Domain.Events;

namespace Shopping.Application;

public interface IEventPublisher
{
    Task Publish(IEvent @event);
}