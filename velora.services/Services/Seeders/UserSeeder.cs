using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using velora.core.Entities.IdentityEntities;

namespace velora.services.Services.Seeders
{
    public static class UserSeeder
    {
        public static async Task SeedDefaultUserAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Person>>();
            var config = serviceProvider.GetRequiredService<IConfiguration>();

            string email = config["Seeding:DefaultUser:Email"]; 
            string password = config["Seeding:DefaultUser:Password"];

            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser == null)
            {
                var newUser = new Person
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newUser, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "User"); 
                }
            }
            else
            {
                var roles = await userManager.GetRolesAsync(existingUser);
                if (!roles.Contains("User"))
                {
                    await userManager.AddToRoleAsync(existingUser, "User");
                }
            }
        }
    }
}
