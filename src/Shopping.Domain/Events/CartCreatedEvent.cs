namespace Shopping.Domain.Events;

public record CartCreatedEvent(string CartId)
    : EventBase("shopping-cart-created"), IEvent
{
    public string Type => "Shopping.Cart.Created.v1";
    public string Source => $"urn:cart:{CartId}";
}