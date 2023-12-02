namespace Shopping.Domain.Events;

public record CartItemQuantityUpdatedEvent(string CartId, int Quantity, string ProductId) 
    : EventBase, IEvent
{
    public string Type => "Shopping.Cart.ItemQuantityUpdated.v1";
    public string Source => $"urn:cart:{CartId}";
    
    public new static readonly string TopicName = "shopping-cart-item-quantity-updated";
    
    public override string? GetTopicName()
    {
        return TopicName;
    }
    
    public override object GetData()
    {
        return new {CartId, ProductId, Quantity};
    }
}