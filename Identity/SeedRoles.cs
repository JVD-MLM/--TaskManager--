using Microsoft.AspNetCore.Identity;
using TaskManager.Domain.Entities.Identity;

namespace TaskManager.Identity;

/// <summary>
///     اضافه کردن نقش ها و ادمین در دیتابیس
/// </summary>
public static class SeedRoles
{
    public static async Task Initialize(RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager)
    {
        string[] roleNames = { "Admin", "Manager", "Employee" };

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist) await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
        }

        var adminUser = new ApplicationUser
        {
            UserName = "test-manager@rayan.com",
            Email = "test-manager@rayan.com",
            FirstName = "Admin",
            LastName = "Admin",
            IsActive = true,
            Gender = UserGender.Female,
            EmailConfirmed = true,
            IsAdmin = true
        };

        var adminPassword = "Developer@Rayan147";

        var user = await userManager.FindByEmailAsync(adminUser.Email);

        if (user == null)
        {
            var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
            if (createAdmin.Succeeded) await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}