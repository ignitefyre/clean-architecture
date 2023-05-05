namespace Shopping.Domain.Events;

public record CartItemAddedEvent(string CartId, int Quantity, string ProductId) : IEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Type => "Cart.Item.Added.v1";
    public string Source => $"url:cart:{CartId}";
    
    public object? GetData()
    {
        return new {CartId, ProductId, Quantity};
    }
}