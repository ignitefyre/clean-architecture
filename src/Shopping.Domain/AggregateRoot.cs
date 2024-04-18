using Shopping.Domain.Events;

namespace Shopping.Domain;

public abstract class AggregateRoot
{
    private readonly List<IEvent> _events = new();
    public IReadOnlyCollection<IEvent> Events => _events.AsReadOnly();

    protected AggregateRoot(string id)
    {
        Id = id;
    }
    public string Id { get; }

    protected void AddEvent(IEvent @event)
    {
        _events.Add(@event);
    }
}