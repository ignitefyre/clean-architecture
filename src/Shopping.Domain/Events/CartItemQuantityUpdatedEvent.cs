namespace Shopping.Domain.Events;

public record CartItemQuantityUpdatedEvent(string CartId, int Quantity, string ProductId) 
    : EventBase("shopping-cart-item-quantity-updated"), IEvent
{
    public string Type => "Shopping.Cart.ItemQuantityUpdated.v1";
    public string Source => $"urn:cart:{CartId}";
    
    public override object GetData()
    {
        return new {CartId, ProductId, Quantity};
    }
}