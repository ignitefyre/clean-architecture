using FluentResults;
using Shopping.Application.Carts;
using Shopping.Domain.Carts;
using Shopping.Domain.Errors;

namespace Shopping.Infrastructure.Carts;

public class CartRepository : ICartRepository
{
    private static readonly List<CartData> _cart = new List<CartData>();
    
    public async Task<Result<Cart>> Create()
    {
        var cart = new CartData();
        _cart.Add(cart);
        return Result.Ok(new Cart(cart.Id));
    }

    public async Task<Result<Cart>> GetById(string id)
    {
        var cart = _cart.FirstOrDefault(x => x.Id == id);

        if (cart == null)
            return Result.Fail(new CartNotFoundError());

        var items = cart.Items.Select(x => new CartItem(x.Id, x.Quantity)).ToList();
        return Result.Ok(new Cart(cart.Id, items, cart.ModifiedOn));
    }

    public async Task<Result> Update(Cart entity)
    {
        var cart = _cart.FirstOrDefault(x => x.Id == entity.Id);

        if (cart == null)
            return Result.Fail(new CartNotFoundError());

        cart.ModifiedOn = DateTime.UtcNow;
        cart.Items = entity.GetItems().Select(x => new CartItemData(x.Id, x.Quantity)).ToList();

        return Result.Ok();
    }
}