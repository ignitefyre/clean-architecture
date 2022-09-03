namespace Shopping.Domain.Carts;

public class Cart : AggregateRoot
{
    public Cart(string id) : base(id) { }
    
    public Cart(string id, ICollection<CartItem> items) : base(id)
    {
        Items = items;
    }
    
    private ICollection<CartItem> Items { get; } = new List<CartItem>();

    public IEnumerable<CartItem> GetItems()
    {
        return Items.ToList();
    }
    
    public void AddItem(string productId, int quantity)
    {
        Items.Add(new CartItem(productId, quantity));
    }
    
    public void UpdateItemQuantity(string productId, int quantity)
    {
        var item = Items.FirstOrDefault(x => x.Id == productId);

        item?.UpdateQuantity(quantity);
    }

    public void RemoveItem(string productId)
    {
        var item = Items.FirstOrDefault(x => x.Id == productId);

        if (item != null)
            Items.Remove(item);
    }
}