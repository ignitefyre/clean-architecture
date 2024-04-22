using AutoMapper;
using FluentResults;
using Shopping.Application;
using Shopping.Application.Carts;
using Shopping.Application.Products;
using Shopping.Domain.Carts;
using Shopping.Domain.Errors;

namespace Shopping.Infrastructure.Carts;

public class CartRepository : ICartRepository
{
    private static readonly List<CartData> CartsInMemory = [];
    
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CartRepository(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<Cart>> Create(string ownerId)
    {
        var cart = new CartData(ownerId);
        CartsInMemory.Add(cart);
        return Result.Ok(new Cart(cart.Id));
    }

    public async Task<Result<Cart>> GetById(string id)
    {
        var cart = CartsInMemory.FirstOrDefault(x => x.Id == id);

        if (cart == null)
            return Result.Fail(new CartNotFoundError());

        foreach (var item in cart.Items)
        {
            var product = await _productRepository.GetById(item.Id);
            
            if(product.IsSuccess)
                cart.Items.First(x => x.Id == item.Id).IncludePrice(product.Value.Price);
        }
        
        return Result.Ok(_mapper.Map<Cart>(cart));
    }

    public async Task<Result> Update(Cart entity)
    {
        var cart = CartsInMemory.FirstOrDefault(x => x.Id == entity.Id);

        if (cart == null)
            return Result.Fail(new CartNotFoundError());

        cart.ModifiedOn = DateTime.UtcNow;
        cart.Items = entity.GetItems().Select(x => new CartItemData(x.Id, x.Quantity)).ToList();

        return Result.Ok();
    }
}