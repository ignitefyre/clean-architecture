using AutoMapper;
using Shopping.Api.Models;
using Shopping.Application.Carts;

namespace Shopping.Api.Mappings;

public class CartResponseProfileV1 : Profile
{
    public CartResponseProfileV1()
    {
        CreateMap<CartDto, CartResponse>()
            .ConstructUsing((src, ctx) =>
                new CartResponse(
                    src.Id,
                    new CartResponseData(
                        src.Id, 
                        ctx.Mapper.Map<ICollection<CartItemDto>, List<CartItem>>(src.Items))));
        
        CreateMap<CartItemDto, CartItem>()
            .ConstructUsing(src => new CartItem(src.Id, src.Quantity));
    }
}