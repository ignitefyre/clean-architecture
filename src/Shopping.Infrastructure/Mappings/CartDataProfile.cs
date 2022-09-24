using AutoMapper;
using Shopping.Application.Carts;
using Shopping.Domain.Carts;
using Shopping.Infrastructure.Carts;

namespace Shopping.Infrastructure.Mappings;

public class CartDataProfile : Profile
{
    public CartDataProfile()
    {
        CreateMap<CartData, Cart>()
            .ConstructUsing((src, ctx) =>
                new Cart(
                    src.Id,
                    ctx.Mapper.Map<ICollection<CartItemData>, ICollection<CartItem>>(src.Items),
                    src.ModifiedOn));
        
        CreateMap<CartItemData, CartItem>()
            .ConstructUsing(src => new CartItem(src.Id, src.Quantity));
    }
}