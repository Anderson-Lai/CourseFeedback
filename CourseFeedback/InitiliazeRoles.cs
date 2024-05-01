using CourseFeedback.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace CourseFeedback
{
    public class InitiliazeRoles
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public InitiliazeRoles(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task InitializeData()
        {
            string[] roles = { "Admin", "User" };

            foreach(string role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            const string email = "admintest@ycdsbk12.ca";
            const string password = "!Test123";

            if(await userManager.FindByEmailAsync(email) is null)
            {
                var admin = new ApplicationUser
                {
                    GraduatingYear = 2026,
                    FirstName = "test",
                    MiddleName = "test",
                    LastName = "test",
                    Email = email,
                };

                await userManager.SetUserNameAsync(admin, email);
                await userManager.CreateAsync(admin, password);
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}

