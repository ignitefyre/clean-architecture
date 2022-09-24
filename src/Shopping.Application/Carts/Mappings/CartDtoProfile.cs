using AutoMapper;
using Shopping.Domain.Carts;

namespace Shopping.Application.Carts.Mappings;

public class CartDtoProfile : Profile
{
    public CartDtoProfile()
    {
        CreateMap<Cart, CartDto>()
            .ConstructUsing((src, ctx) =>
                new CartDto(
                    src.Id, 
                    ctx.Mapper.Map<ICollection<CartItem>, ICollection<CartItemDto>>(src.GetItems().ToList()),
                    src.ModifiedOn
                    ));

        CreateMap<CartItem, CartItemDto>()
            .ConstructUsing((src, ctx) => 
                new CartItemDto(
                    src.Id, src.Quantity
                ));
    }
}