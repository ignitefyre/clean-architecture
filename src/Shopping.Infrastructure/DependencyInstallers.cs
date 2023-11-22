using Microsoft.Extensions.DependencyInjection;
using Shopping.Application;
using Shopping.Application.Carts;
using Shopping.Application.Products;
using Shopping.Infrastructure.Carts;
using Shopping.Infrastructure.Mappings;
using Shopping.Infrastructure.Products;

namespace Shopping.Infrastructure;

public static class DependencyInstallers
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ICartRepository, CartRepository>();
        services.AddSingleton<IProductRepository, ProductRepository>();
        services.AddSingleton<IEventPublisher, EventPublisher>();

        services.AddAutoMapper(typeof(CartDataProfile));
        services.AddAutoMapper(typeof(ProductDataProfile));
        
        return services;
    }
}