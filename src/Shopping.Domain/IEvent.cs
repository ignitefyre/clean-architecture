namespace Shopping.Domain;

public interface IEvent
{
    Guid Id { get; }
    string Type { get; }
    string Source { get; }
    object? GetData();
    string? GetTopicName();
}