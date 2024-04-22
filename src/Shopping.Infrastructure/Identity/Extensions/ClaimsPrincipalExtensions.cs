using System.Security.Claims;

namespace Shopping.Infrastructure.Identity.Extensions;

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