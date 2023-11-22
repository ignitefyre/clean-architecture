namespace Shopping.Domain.Products;

public class Product : AggregateRoot
{
    public Product(string id, double price) : base(id)
    {
        Price = price;
    }
    
    public double Price { get; }
}