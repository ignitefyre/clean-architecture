using FluentResults;

namespace Shopping.Domain.Errors;

public class CartNotFoundError : Error
{
    public CartNotFoundError() : base("Cart not found")
    {
        Metadata.Add("code", 404);
    }
}