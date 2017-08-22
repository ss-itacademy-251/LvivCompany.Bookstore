using LvivCompany.Bookstore.DataAccess;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Web.Mapper;
using LvivCompany.Bookstore.Web.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Identity;

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
            var config = builder.Build();
            builder.AddAzureKeyVault(
            $"https://{config["azureKeyVault:vault"]}.vault.azure.net/",
            config["azureKeyVault:clientId"],
            config["azureKeyVault:clientSecret"]);

            Configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionToIdentityDb")));


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

            services.AddSession();

            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionToDb")));

            services.AddTransient<IRepo<Book>, BookRepository>();
            services.AddTransient<IRepo<Author>, AuthorRepository>();
            services.AddTransient<IRepo<Category>, CategoryRepository>();
            services.AddTransient<IRepo<Order>, OrderRepository>();
            services.AddTransient<IRepo<OrderDetail>, OrderDetailRepository>();
            services.AddTransient<IRepo<Publisher>, PublisherRepository>();
            services.AddTransient<IRepo<Status>, StatusRepository>();

            services.AddTransient<IMapper<Book, BookViewModel>, BookMapper>();
            services.AddTransient<IMapper<Book, EditBookViewModel>, EditBookMapper>();
            services.AddSingleton(Configuration);
            services.AddTransient<IMapper<User, EditProfileViewModel>, ProfileMapper>();
            services.AddTransient<IMapper<User, RegisterViewModel>, RegisterMapper>();
            services.AddTransient<IMapper<OrderDetail, OrderViewModel>, OrderMapper>();

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

            app.UseSession();
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