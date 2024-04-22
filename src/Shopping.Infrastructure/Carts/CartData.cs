namespace Shopping.Infrastructure.Carts;

public class CartData
{
    public CartData(string ownerId)
    {
        OwnerId = ownerId;
        Id = Guid.NewGuid().ToString();
        ModifiedOn = DateTime.UtcNow;
    }
    
    public string Id { get; }
    
    public DateTime ModifiedOn { get; set; }

    public ICollection<CartItemData> Items { get; set; } = new List<CartItemData>();
    
    public string OwnerId { get; set; }
}