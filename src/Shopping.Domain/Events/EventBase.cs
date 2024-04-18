namespace Shopping.Domain.Events;

public abstract record EventBase
{
    public virtual Guid Id { get; } = Guid.NewGuid();
    public virtual object? GetData() => null;

    public static readonly string? TopicName = default;
    
    public virtual string? GetTopicName() => TopicName;
}