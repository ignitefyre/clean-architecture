namespace Shopping.Domain.Events;

public record CartItemRemovedEvent(string CartId, string ProductId)
    : EventBase, IEvent
{
    public string Type => "Shopping.Cart.ItemRemoved.v1";
    public string Source => $"urn:cart:{CartId}";
    
    public new static readonly string TopicName = "shopping-cart-item-removed";
    
    public override string? GetTopicName()
    {
        return TopicName;
    }

    public override object GetData()
    {
        return new {CartId, ProductId};
    }
}