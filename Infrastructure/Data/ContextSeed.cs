

using Microsoft.AspNetCore.Identity;
using Task_ProjectManagementAPI.Domain.Entities;

namespace Infrastructure.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Manager", "User" };
            foreach(var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        public static async Task CreateAsmin(UserManager<User> userManager)
        {
            if(await userManager.FindByEmailAsync("admin@project.com") is null)
            {
                string password = "Admin@123";
                var user = new User
                {
                    UserName = "Admin",
                    Email = "admin@project.com"
                };

                await userManager.CreateAsync(user, password);
                var resule = await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
