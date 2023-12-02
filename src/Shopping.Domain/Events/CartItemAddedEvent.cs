namespace Shopping.Domain.Events;

public record CartItemAddedEvent(string CartId, int Quantity, string ProductId) 
    : EventBase("shopping-cart-item-added"), IEvent
{
    public string Type => "Shopping.Cart.ItemAdded.v1";
    public string Source => $"urn:cart:{CartId}";
    
    public override object GetData()
    {
        return new {CartId, ProductId, Quantity};
    }
}