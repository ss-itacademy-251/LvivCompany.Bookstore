using LvivCompany.Bookstore.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LvivCompany.Bookstore.BusinessLogic
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(config.GetConnectionString("Lv251BookstoreDb")));
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(config.GetConnectionString("IdentityDb")));
            return services;
        }
    }
}