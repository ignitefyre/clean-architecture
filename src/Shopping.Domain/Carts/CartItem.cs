namespace Shopping.Domain.Carts;

public class CartItem
{
    public string Id { get; }
    public int Quantity { get; private set; }
    
    public double PricePerItem { get; private set; }

    public double ItemTotal => PricePerItem * Quantity;

    public CartItem(string id, int quantity, double price = 0)
    {
        Id = id;
        Quantity = quantity;
        PricePerItem = price;
    }

    public void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }
}