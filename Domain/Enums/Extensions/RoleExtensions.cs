using TaskManager.Identity;

namespace TaskManager.Domain.Enums.Extensions;

public static class RoleExtensions
{
    public static string GetName(this UserRoles role)
    {
        return Enum.GetName(typeof(UserRoles), role);
    }
}