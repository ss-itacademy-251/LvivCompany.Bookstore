using LvivCompany.Bookstore.DataAccess;
using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LvivCompany.Bookstore.Web
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IHostingEnvironment env, IConfiguration config)
        {
            if (env.IsDevelopment())
            {
                services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnectionToDb")));
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("DefaultConnectionToIdentityDb")));
            }
            else
            {
                services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(config["appSettings:connectionStrings:lv251bookstore"]));
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(config["appSettings:connectionStrings:IdentityDb"]));
            }

            return services;
        }
    }
}