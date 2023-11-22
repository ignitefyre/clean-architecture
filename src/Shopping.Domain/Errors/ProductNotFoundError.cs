using FluentResults;

namespace Shopping.Domain.Errors;

public class ProductNotFoundError : Error
{
    public ProductNotFoundError() : base("Product not found")
    {
        Metadata.Add("code", 404);
    }
}