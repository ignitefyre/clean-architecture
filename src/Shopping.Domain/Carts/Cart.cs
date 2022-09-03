namespace Shopping.Domain.Carts;

public class Cart : AggregateRoot
{
    public Cart(string id) : base(id) { }
}