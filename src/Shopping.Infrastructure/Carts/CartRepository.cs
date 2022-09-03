using FluentResults;
using Shopping.Application.Carts;
using Shopping.Domain.Carts;
using Shopping.Domain.Errors;

namespace Shopping.Infrastructure.Carts;

public class CartRepository : ICartRepository
{
    private static readonly List<CartData> _cart = new List<CartData>();
    
    public Result<Cart> Create()
    {
        var cart = new CartData();
        _cart.Add(cart);
        return Result.Ok(new Cart(cart.Id));
    }

    public Result<Cart> GetById(string id)
    {
        var cart = _cart.FirstOrDefault(x => x.Id == id);

        if (cart == null)
            return Result.Fail(new CartNotFoundError());

        var items = cart.Items.Select(x => new CartItem(x.Id, x.Quantity)).ToList();
        return Result.Ok(new Cart(cart.Id, items));
    }

    public Result Update(Cart entity)
    {
        var cart = _cart.FirstOrDefault(x => x.Id == entity.Id);

        if (cart == null)
            return Result.Fail(new CartNotFoundError());

        cart.Items = entity.GetItems().Select(x => new CartItemData(x.Id, x.Quantity)).ToList();

        return Result.Ok();
    }
}