using CourseFeedback.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace CourseFeedback
{
    public class SeedUserData
    {
        public static async Task SeedRolesAndAdminAccount(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // roles

            string[] roles = ["Admin", "User"];

            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // admin account

            const string email = "admin@ycdsbk12.ca";
            const string password = "!Test123";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var admin = new ApplicationUser
                {
                    FirstName = "Admin",
                    MiddleName = "admin",
                    LastName = "Admin",
                    GraduatingYear = 0000,
                    Email = email,
                };

                await userManager.SetUserNameAsync(admin, email);
                await userManager.CreateAsync(admin, password);
                await userManager.AddToRoleAsync(admin, "Admin");
            }

        }
    }
}