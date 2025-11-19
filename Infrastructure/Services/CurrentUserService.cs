using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskManager.Application.IServices;

namespace TaskManager.Infrastructure.Services;

/// <summary>
///     سرویس دریافت کاربر جاری برای DbContext
/// </summary>
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId =>
        Guid.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier),
            out var id)
            ? id
            : null;
}