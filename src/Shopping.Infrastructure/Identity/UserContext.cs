using Microsoft.AspNetCore.Http;
using Shopping.Application;
using Shopping.Infrastructure.Identity.Extensions;

namespace Shopping.Infrastructure.Identity;

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