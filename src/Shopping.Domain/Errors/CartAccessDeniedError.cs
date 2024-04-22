using FluentResults;

namespace Shopping.Domain.Errors;

public class CartAccessDeniedError : Error
{
    public CartAccessDeniedError() : base("Access denied to cart")
    {
        Metadata.Add("code", 401);
    }
}