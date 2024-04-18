using Shopping.Domain.Events;

namespace Shopping.Domain.Carts;

public class Cart : AggregateRoot
{
    public Cart() : base(Guid.NewGuid().ToString())
    {
        AddEvent(new CartCreatedEvent(Id));
    }
    
    public Cart(string id) : base(id) { }
    
    public Cart(string id, ICollection<CartItem> items, DateTime modifiedOn) : base(id)
    {
        Items = items;
        ModifiedOn = modifiedOn;
    }

    public double Total => Items.Sum(x => x.ItemTotal);
    
    public DateTime ModifiedOn { get; private set; }
    
    private ICollection<CartItem> Items { get; } = new List<CartItem>();

    public IEnumerable<CartItem> GetItems()
    {
        return Items.ToList();
    }
    
    public void AddItem(string productId, int quantity)
    {
        Items.Add(new CartItem(productId, quantity));
        
        AddEvent(new CartItemAddedEvent(Id, quantity, productId));
    }
    
    public void UpdateItemQuantity(string productId, int quantity)
    {
        var item = Items.FirstOrDefault(x => x.Id == productId);

        item?.UpdateQuantity(quantity);
        
        AddEvent(new CartItemQuantityUpdatedEvent(Id, quantity, productId));
    }

    public void RemoveItem(string productId)
    {
        var item = Items.FirstOrDefault(x => x.Id == productId);

        if (item == null) return;
        Items.Remove(item);
        AddEvent(new CartItemRemovedEvent(Id, productId));
    }
}