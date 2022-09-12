using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Application.Carts.Mappings;

namespace Shopping.Application;

public static class DependencyInstallers
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(
            typeof(CartDtoProfile));
        
        return services;
    }   
}