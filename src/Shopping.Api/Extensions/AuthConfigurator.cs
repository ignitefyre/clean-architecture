using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Shopping.Api.Extensions;

public static class AuthConfigurator
{
    public static IServiceCollection AddEndpointPolicies(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("RequireWriteCarts", policy =>
                policy.RequireAssertion(context =>
                {
                    var scopeClaim = context.User.FindFirst("scope")?.Value ?? "";
                    var scopes = scopeClaim.Split(' ');
                    return scopes.Contains("write:carts");
                }))
            .AddPolicy("RequireReadCarts", policy =>
                policy.RequireAssertion(context =>
                {
                    var scopeClaim = context.User.FindFirst("scope")?.Value ?? "";
                    var scopes = scopeClaim.Split(' ');
                    return scopes.Contains("read:carts");
                }));
        
        return services;
    }
    
    public static IServiceCollection AddAuthentication(this IServiceCollection services, string? authority, string? audience)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(authority, nameof(authority));
        ArgumentException.ThrowIfNullOrWhiteSpace(audience, nameof(audience));
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.Audience = audience;
            });
        
        return services;
    }
}