namespace Shopping.Infrastructure.Products;

public class ProductData
{
    public ProductData(string id, double price)
    {
        Id = id;
        Price = price;
    }
    public string Id { get; }
    public double Price { get; }
}