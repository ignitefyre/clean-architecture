using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Application.Carts.Mappings;

namespace Shopping.Application;

public static class DependencyInstallers
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg 
            => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(typeof(CartDtoProfile));
        
        return services;
    }   
}