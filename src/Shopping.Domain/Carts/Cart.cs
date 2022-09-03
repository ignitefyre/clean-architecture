namespace Shopping.Domain.Carts;

public class Cart : AggregateRoot
{
    public Cart(string id) : base(id) { }
    
    public Cart(string id, ICollection<CartItem> items) : base(id)
    {
        Items = items;
    }
    
    private ICollection<CartItem> Items { get; } = new List<CartItem>();

    public List<CartItem> GetItems()
    {
        return Items.ToList();
    }
}