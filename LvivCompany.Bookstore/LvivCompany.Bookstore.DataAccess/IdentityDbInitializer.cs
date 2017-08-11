using System;
using System.Collections.Generic;
using System.Text;
using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
namespace LvivCompany.Bookstore.DataAccess
{

    //public static class IdentityDbInitializer
    //{
    //    public static async void Initialize(IServiceProvider services, IConfiguration Configuration)
    //    {
    //        string[] roles = new string[] {
    //            "Admin",
    //            "Seller",
    //            "Customer"
    //        };
    //        using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
    //        {
    //            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<long>>>();
    //            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    //            foreach (string role in roles)
    //            {
    //                if (!await roleManager.RoleExistsAsync(role))
    //                {
    //                    var result = await roleManager.CreateAsync(new IdentityRole<long> { Name = role });
    //                }
    //            }               
    //        }
    //    }
    //}
}


