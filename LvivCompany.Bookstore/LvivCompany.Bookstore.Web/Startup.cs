using LvivCompany.Bookstore.BusinessLogic;
using LvivCompany.Bookstore.BusinessLogic.Mapper;
using LvivCompany.Bookstore.BusinessLogic.ViewModels;
using LvivCompany.Bookstore.DataAccess;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LvivCompany.Bookstore.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            var config = builder.Build();
            builder.AddAzureKeyVaults(env, config);
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContexts(Configuration);
            services.AddIdentity<User, Role>(o =>
            {
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.Password.RequireUppercase = false;
            })
               .AddEntityFrameworkStores<ApplicationContext>()
               .AddDefaultTokenProviders();
            services.AddScoped<RoleManager<Role>>();
            services.AddMvc();
            services.AddScoped<IRepo<Book>, BookRepository>();
            services.AddScoped<IRepo<Author>, AuthorRepository>();
            services.AddScoped<IRepo<Category>, CategoryRepository>();
            services.AddScoped<IRepo<Order>, OrderRepository>();
            services.AddScoped<IRepo<OrderDetail>, OrderDetailRepository>();
            services.AddScoped<IRepo<Publisher>, PublisherRepository>();
            services.AddScoped<IRepo<Status>, StatusRepository>();
            services.AddScoped<IMapper<Book, BookViewModel>, BookMapper>();
            services.AddScoped<IMapper<Book, EditBookViewModel>, EditBookMapper>();
            services.AddScoped<IMapper<OrderDetail, OrderHistoryViewModel>, OrderHistoryMapper>();
            services.AddSession();
            services.AddSingleton(Configuration);
            services.AddScoped<IMapper<User, EditProfileViewModel>, ProfileMapper>();
            services.AddScoped<IMapper<User, RegisterViewModel>, RegisterMapper>();
            services.AddScoped<IMapper<User, EditProfileViewModel>, ProfileMapper>();
            services.AddScoped<IMapper<User, RegisterViewModel>, RegisterMapper>();
            services.AddScoped<BookServices>();
            services.AddScoped<HomeServices>();
            services.AddScoped<SearchServices>();
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