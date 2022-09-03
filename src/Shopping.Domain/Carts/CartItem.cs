namespace Shopping.Domain.Carts;

public class CartItem
{
    public string Id { get; }
    public int Quantity { get; private set; }

    public CartItem(string id, int quantity)
    {
        Id = id;
        Quantity = quantity;
    }

    public void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }
}