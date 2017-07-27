using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using LvivCompany.Bookstore.Entities.Models;

namespace LvivCompany.Bookstore.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }       

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionToIdentityDb")));

            services.AddIdentity<User,AppRole>()
               .AddEntityFrameworkStores<ApplicationContext>()
               .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionToDb")));
            services.AddTransient<IRepo<Book>, BookRepository>();
            services.AddTransient<IRepo<Author>, AuthorRepository>();
            services.AddTransient<IRepo<Category>, CategoryRepository>();
            services.AddTransient<IRepo<Order>, OrderRepository>();
            services.AddTransient<IRepo<OrderDetail>, OrderDetailRepository>();
            services.AddTransient<IRepo<Publisher>, PublisherRepository>();
            services.AddTransient<IRepo<Status>, StatusRepository>();

            var serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetService<BookStoreContext>();
            DbInitializer.Seed(context);
            return serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, BookStoreContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();         

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
