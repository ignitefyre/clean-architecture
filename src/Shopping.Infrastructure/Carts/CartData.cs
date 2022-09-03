namespace Shopping.Infrastructure.Carts;

public class CartData
{
    public CartData()
    {
        Id = Guid.NewGuid().ToString();
    }
    
    public CartData(string initialCartId)
    {
        Id = initialCartId;
    }
    public string Id { get; }

    public ICollection<CartItemData> Items { get; set; } = new List<CartItemData>();
}