namespace Shopping.Infrastructure.Carts;

public class CartData
{
    public CartData(string ownerName)
    {
        OwnerName = ownerName;
        Id = Guid.NewGuid().ToString();
        ModifiedOn = DateTime.UtcNow;
    }
    
    public string Id { get; }
    
    public DateTime ModifiedOn { get; set; }

    public ICollection<CartItemData> Items { get; set; } = new List<CartItemData>();
    
    public string OwnerName { get; set; }
}