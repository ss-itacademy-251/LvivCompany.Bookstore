using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LvivCompany.Bookstore.DataAccess
{

    public static class IdentityDbInitializer
    {
        public static async void Initialize(IServiceProvider services, IConfiguration Configuration)
        {
            string[] roles = new string[] {
                "Admin",
                "Seller",
                "Customer"
            };
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                foreach (string role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        var result = await roleManager.CreateAsync(new Role { Name = role });
                    }
                }
            }
        }
    }
}