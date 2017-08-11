using LvivCompany.Bookstore.DataAccess;
using LvivCompany.Bookstore.DataAccess.IRepo;
using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LvivCompany.Bookstore.Web.ViewModels;
using LvivCompany.Bookstore.Web.Mapper;
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
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)            
                .AddEnvironmentVariables();

            var config = builder.Build();

            builder.AddAzureKeyVault(
               $"https://{config["azureKeyVault:vault"]}.vault.azure.net/",
               config["azureKeyVault:clientId"],
               config["azureKeyVault:clientSecret"]);

            Configuration = builder.Build();
          

            var connectionString = Configuration["appSettings--connectionStrings--bookstore2Db"];
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<User, IdentityRole<long>>(o =>
            {
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.Password.RequireUppercase = false;
            })
               .AddEntityFrameworkStores<ApplicationContext>()
               .AddDefaultTokenProviders();
            services.Configure<Config.AppConfiguration>(Configuration.GetSection("AppSettings"));
            services.AddScoped<RoleManager<IdentityRole<long>>, RoleManager<IdentityRole<long>>>();

            services.AddMvc();

            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("bookstore")));


            services.AddTransient<IRepo<Book>, BookRepository>();
            services.AddTransient<IRepo<Author>, AuthorRepository>();
            services.AddTransient<IRepo<Category>, CategoryRepository>();
            services.AddTransient<IRepo<Order>, OrderRepository>();
            services.AddTransient<IRepo<OrderDetail>, OrderDetailRepository>();
            services.AddTransient<IRepo<Publisher>, PublisherRepository>();
            services.AddTransient<IRepo<Status>, StatusRepository>();

            services.AddTransient<IMapper<Book, BookDetailViewModel>, BookDetailMapper>();
            services.AddTransient<IMapper<Book, BookInfo>, BookMapper>();
            services.AddSingleton(Configuration);
            services.AddTransient<IMapper<User, EditProfileViewModel>, ProfileMapper>();
            services.AddTransient<IMapper<User, RegisterViewModel>, RegisterMapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

