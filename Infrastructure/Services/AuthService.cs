using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskManager.Application.IServices;

namespace TaskManager.Infrastructure.Services;

/// <summary>
///     سرويس احراز هويت
/// </summary>
public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? GetCurrentUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        return string.IsNullOrEmpty(userId) ? null : Guid.Parse(userId);
    }
}