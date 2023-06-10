namespace Shopping.Domain.Events;

public record CartItemRemovedEvent(string CartId, string ProductId) : IEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Type => "Shopping.Cart.ItemRemoved.v1";
    public string Source => $"urn:cart:{CartId}";
    
    public object? GetData()
    {
        return new {CartId, ProductId};
    }
}