namespace Shopping.Infrastructure.Carts;

public class CartItemData
{
    public CartItemData(string id, int quantity)
    {
        Id = id;
        Quantity = quantity;
    }

    public string Id { get; }
    public int Quantity { get; }
    public double Price { get; private set; }

    public void IncludePrice(double value)
    {
        Price = value;
    }
}