namespace Shopping.Domain.Events;

public record CartCreatedEvent(string CartId)
    : EventBase, IEvent
{
    public string Type => "Shopping.Cart.Created.v1";
    public string Source => $"urn:cart:{CartId}";

    public new static readonly string TopicName = "shopping-cart-created";

    public override string? GetTopicName()
    {
        return TopicName;
    }
}