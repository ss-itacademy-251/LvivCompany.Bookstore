using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LvivCompany.Bookstore.DataAccess
{
    public static class IdentityDbInitializer
    {
        public static async void Initialize(IServiceProvider services)
        {
            string[] roles = new string[]
            {
                "Admin",
                "Seller",
                "Customer"
            };

            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
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