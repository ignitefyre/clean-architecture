using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Shopping.Application;

namespace Shopping.Infrastructure;

internal sealed class UserContext(IHttpContextAccessor httpContextAccessor)
    : IUserContext
{
    public bool IsAuthenticated =>
        httpContextAccessor
            .HttpContext?
            .User
            .Identity?
            .IsAuthenticated ??
        throw new ApplicationException("User context is unavailable");


    public string UserId =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new ApplicationException("User context is unavailable");
    
    public string Name =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserName() ??
        throw new ApplicationException("User context is unavailable");
    
    public string Title { get; }
}

internal static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
    }
    
    public static string GetUserName(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirst("usr.name")?.Value ?? string.Empty;
    }
}
