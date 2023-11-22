using AutoMapper;
using Shopping.Domain.Products;
using Shopping.Infrastructure.Products;

namespace Shopping.Infrastructure.Mappings;

public class ProductDataProfile : Profile
{
    public ProductDataProfile()
    {
        CreateMap<ProductData, Product>()
            .ConstructUsing((src, ctx) =>
                new Product(
                    src.Id,
                    src.Price));
    }
}