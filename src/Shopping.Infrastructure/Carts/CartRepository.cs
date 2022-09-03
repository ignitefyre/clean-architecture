using FluentResults;
using Shopping.Application.Carts;
using Shopping.Domain.Carts;

namespace Shopping.Infrastructure.Carts;

public class CartRepository : ICartRepository
{
    public Result<Cart> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public Result Update(Cart entity)
    {
        throw new NotImplementedException();
    }
}