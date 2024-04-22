using AutoMapper;
using Shopping.Api.Models;
using Shopping.Application.Carts;

namespace Shopping.Api.Mappings;

public class CartResponseProfile : Profile
{
    public CartResponseProfile()
    {
        CreateMap<CartDto, CartResponse>()
            .ConstructUsing((src, ctx) =>
                new CartResponse(
                    src.Id,
                    new CartResponseData(
                        src.Id,
                        src.Total,
                        ctx.Mapper.Map<ICollection<CartItemDto>, List<CartItem>>(src.Items)), 
                    src.OwnerName))
            .ForPath(x => x.Data.Updated, r => r.MapFrom(src => src.ModifiedOn));
        
        CreateMap<CartItemDto, CartItem>()
            .ConstructUsing(src => new CartItem(
                src.Id,
                src.Quantity,
                src.Price));
    }
}