namespace Shopping.Domain.Events;

public record CartItemQuantityUpdatedEvent(string CartId, int Quantity, string ProductId) : IEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Type => "Shopping.Cart.ItemQuantityUpdated.v1";
    public string Source => $"urn:cart:{CartId}";
    
    public object? GetData()
    {
        return new {CartId, ProductId, Quantity};
    }
}