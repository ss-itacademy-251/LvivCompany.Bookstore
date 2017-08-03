using AutoMapper;
using LvivCompany.Bookstore.DataAccess;
using LvivCompany.Bookstore.DataAccess.Repo;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Entities.Models;
using LvivCompany.Bookstore.Web.Mapper;
using LvivCompany.Bookstore.Web.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionToIdentityDb")));

            services.AddIdentity<User,AppRole>()
               .AddEntityFrameworkStores<ApplicationContext>()
               .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddAutoMapper();
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionToDb")));

            services.AddTransient<IRepo<Book>, BookRepository>();
            services.AddTransient<IRepo<Author>, AuthorRepository>();
            services.AddTransient<IRepo<Category>, CategoryRepository>();
            services.AddTransient<IRepo<Order>, OrderRepository>();
            services.AddTransient<IRepo<OrderDetail>, OrderDetailRepository>();
            services.AddTransient<IRepo<Publisher>, PublisherRepository>();
            services.AddTransient<IRepo<Status>, StatusRepository>();

            services.AddTransient<IMapper<Book,BookDetailViewModel>, BookDetailMapper>();
            services.AddTransient<IMapper<Book, BookInfo>, BookMapper>();

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

            app.UseIdentity();         

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