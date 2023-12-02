namespace Shopping.Domain.Events;

public record CartItemAddedEvent(string CartId, int Quantity, string ProductId) 
    : EventBase, IEvent
{
    public string Type => "Shopping.Cart.ItemAdded.v1";
    public string Source => $"urn:cart:{CartId}";
    
    public new static readonly string TopicName = "shopping-cart-item-added";
    
    public override string? GetTopicName()
    {
        return TopicName;
    }
    
    public override object GetData()
    {
        return new {CartId, ProductId, Quantity};
    }
}