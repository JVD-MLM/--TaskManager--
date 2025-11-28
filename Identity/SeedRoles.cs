using Microsoft.AspNetCore.Identity;
using TaskManager.Domain.Entities.Identity;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Enums.Extensions;

namespace TaskManager.Identity;

/// <summary>
///     اضافه کردن نقش ها و ادمین در دیتابیس
/// </summary>
public static class SeedRoles
{
    public static async Task Initialize(RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager)
    {
        var roles = Enum.GetValues(typeof(UserRoles)).Cast<UserRoles>().ToList(); // گرفتن لیست نقش های enum

        foreach (var role in roles) // ایجاد نقش ها در دیتابیس
        {
            var roleName = role.GetName();

            var roleExist = await roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = roleName
                });
        }

        var adminUser = new ApplicationUser
        {
            NationalCode = "1234567890",
            UserName = "1234567890",
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

            if (createAdmin.Succeeded) await userManager.AddToRoleAsync(adminUser, UserRoles.Admin.GetName());
        }
    }
}