using IntegrationLoggerViewer.Enums;
using Microsoft.AspNetCore.Identity;

namespace IntegrationLoggerViewer.Data;

public static class ContextSeed
{
    public static async Task SeedRolesAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Roles
        await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
    }
    public static async Task SeedSuperAdminAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Default User
        var defaultUser = new IdentityUser
        {
            UserName = "superadmin",
            Email = "superadmin@gmail.com",
            NormalizedUserName = "SUPERADMIN",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };
        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "!Lucas@2022#");
                await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                await userManager.AddToRoleAsync(defaultUser, Roles.Moderator.ToString());
                await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
            }
        }
    }
}