using LvivCompany.Bookstore.DataAccess;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.Mapper;
using LvivCompany.Bookstore.Web.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace LvivCompany.Bookstore.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            var config = builder.Build();
            builder.AddAzureKeyVault(
               $"https://{config["azureKeyVaultforDB:vault"]}.vault.azure.net/",
               config["azureKeyVaultforDB:clientId"],
               config["azureKeyVaultforDB:clientSecret"]);
            builder.AddAzureKeyVault(
               $"https://{config["azureKeyVault:vault"]}.vault.azure.net/",
               config["azureKeyVault:clientId"],
               config["azureKeyVault:clientSecret"]);
            Configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var connectionStringToDb = Configuration["appSettings:connectionStrings:lv251bookstore"];
            var connectionStringToIdentityDb = Configuration["appSettings:connectionStrings:IdentityDb"];
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionStringToIdentityDb));
            services.AddIdentity<User, IdentityRole<long>>(o =>
            {
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.Password.RequireUppercase = false;
            })
               .AddEntityFrameworkStores<ApplicationContext>()
               .AddDefaultTokenProviders();
            services.AddScoped<RoleManager<IdentityRole<long>>, RoleManager<IdentityRole<long>>>();
            services.AddMvc();
            services.AddDbContext<BookStoreContext>(options =>
                options.UseSqlServer(connectionStringToDb));
            services.AddScoped<IRepo<Book>, BookRepository>();
            services.AddScoped<IRepo<Author>, AuthorRepository>();
            services.AddScoped<IRepo<Category>, CategoryRepository>();
            services.AddScoped<IRepo<Order>, OrderRepository>();
            services.AddScoped<IRepo<OrderDetail>, OrderDetailRepository>();
            services.AddScoped<IRepo<Publisher>, PublisherRepository>();
            services.AddScoped<IRepo<Status>, StatusRepository>();
            services.AddScoped<IMapper<Book, BookViewModel>, BookMapper>();
            services.AddScoped<IMapper<Book, EditBookViewModel>, EditBookMapper>();
            services.AddSingleton(Configuration);
            services.AddScoped<IMapper<User, EditProfileViewModel>, ProfileMapper>();
            services.AddScoped<IMapper<User, RegisterViewModel>, RegisterMapper>();
            var serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetService<BookStoreContext>();
            DbInitializer.Seed(context);
            return serviceProvider;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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
            app.UseAuthentication();
            IdentityDbInitializer.Initialize(app.ApplicationServices, Configuration);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}