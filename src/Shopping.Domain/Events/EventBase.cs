namespace Shopping.Domain.Events;

public abstract record EventBase(string Topic)
{
    public virtual Guid Id { get; } = Guid.NewGuid();
    public virtual object? GetData() => null;
}