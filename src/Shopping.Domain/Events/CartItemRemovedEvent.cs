namespace Shopping.Domain.Events;

public record CartItemRemovedEvent(string CartId, string ProductId)
    : EventBase("shopping-cart-item-removed"), IEvent
{
    public string Type => "Shopping.Cart.ItemRemoved.v1";
    public string Source => $"urn:cart:{CartId}";
    
    public override object GetData()
    {
        return new {CartId, ProductId};
    }
}