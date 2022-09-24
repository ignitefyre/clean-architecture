using Microsoft.Extensions.DependencyInjection;
using Shopping.Application.Carts;
using Shopping.Infrastructure.Carts;
using Shopping.Infrastructure.Mappings;

namespace Shopping.Infrastructure;

public static class DependencyInstallers
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ICartRepository, CartRepository>();

        services.AddAutoMapper(typeof(CartDataProfile));
        
        return services;
    }
}