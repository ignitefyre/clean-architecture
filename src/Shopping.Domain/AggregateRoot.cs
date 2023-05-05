namespace Shopping.Domain;

public abstract class AggregateRoot
{
    private List<IEvent> _events = new();
    public IReadOnlyCollection<IEvent> Events => _events.AsReadOnly();

    public AggregateRoot(string id)
    {
        Id = id;
    }
    public string Id { get; }

    public void AddEvent(IEvent @event)
    {
        _events.Add(@event);
    }
}