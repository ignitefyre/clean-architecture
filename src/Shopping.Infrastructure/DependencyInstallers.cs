using Microsoft.Extensions.DependencyInjection;
using Shopping.Application.Carts;
using Shopping.Infrastructure.Carts;

namespace Shopping.Infrastructure;

public static class DependencyInstallers
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ICartRepository, CartRepository>();
        //services.AddScoped<IEventRepository, EventRepository>();
        
        return services;
    }
}