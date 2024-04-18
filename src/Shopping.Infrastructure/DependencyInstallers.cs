using Microsoft.Extensions.DependencyInjection;
using Shopping.Application;
using Shopping.Application.Carts;
using Shopping.Application.Products;
using Shopping.Infrastructure.Carts;
using Shopping.Infrastructure.Configurators;
using Shopping.Infrastructure.Handlers;
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

        services.AddSingleton<KafkaHandler>();

        services.AddAutoMapper(typeof(CartDataProfile));
        services.AddAutoMapper(typeof(ProductDataProfile));
        
        // Setup Kafka Topics
        new KafkaConfigurator()
            .WithPartitionCount(2)
            .Configure();
        
        return services;
    }
}