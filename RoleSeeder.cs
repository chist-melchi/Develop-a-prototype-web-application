using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgricultureEnergyConnect.Data
{
    public static class RoleSeeder
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "Farmer", "Employee" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Create the roles and seed them to the database
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create a default Employee user
            var employeeUser = new IdentityUser
            {
                UserName = "employee@example.com",
                Email = "employee@example.com"
            };

            string employeePassword = "Employee@123";
            var user = await userManager.FindByEmailAsync("employee@example.com");

            if (user == null)
            {
                var createEmployeeUser = await userManager.CreateAsync(employeeUser, employeePassword);
                if (createEmployeeUser.Succeeded)
                {
                    // Assign the Employee role to the default user
                    await userManager.AddToRoleAsync(employeeUser, "Employee");
                }
            }
        }
    }
}
