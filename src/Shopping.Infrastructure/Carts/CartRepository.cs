using AutoMapper;
using FluentResults;
using Shopping.Application.Carts;
using Shopping.Domain.Carts;
using Shopping.Domain.Errors;

namespace Shopping.Infrastructure.Carts;

public class CartRepository : ICartRepository
{
    private readonly IMapper _mapper;
    private static readonly List<CartData> _cartsInMemory = new List<CartData>();

    public CartRepository(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public async Task<Result<Cart>> Create()
    {
        var cart = new CartData();
        _cartsInMemory.Add(cart);
        return Result.Ok(new Cart(cart.Id));
    }

    public async Task<Result<Cart>> GetById(string id)
    {
        var cart = _cartsInMemory.FirstOrDefault(x => x.Id == id);

        return cart == null ? Result.Fail(new CartNotFoundError()) : Result.Ok(_mapper.Map<Cart>(cart));
    }

    public async Task<Result> Update(Cart entity)
    {
        var cart = _cartsInMemory.FirstOrDefault(x => x.Id == entity.Id);

        if (cart == null)
            return Result.Fail(new CartNotFoundError());

        cart.ModifiedOn = DateTime.UtcNow;
        cart.Items = entity.GetItems().Select(x => new CartItemData(x.Id, x.Quantity)).ToList();

        return Result.Ok();
    }
}