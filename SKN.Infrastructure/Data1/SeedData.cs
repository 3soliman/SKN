using Microsoft.AspNetCore.Identity;
using SKN.Core.Entities;

namespace SKN.Infrastructure.Data1
{
    public static class SeedData
    {
        public static async Task Initialize(ApplicationDbContext context, 
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            // إنشاء الأدوار
            string[] roleNames = { "Admin", "User", "HotelOwner" };
            
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // إنشاء مستخدم admin إذا لم يكن موجوداً
            if (await userManager.FindByEmailAsync("admin@skn.com") == null)
            {
                var adminUser = new User
                {
                    UserName = "admin@skn.com",
                    Email = "admin@skn.com",
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            await context.SaveChangesAsync();
        }
    }
}