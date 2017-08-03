﻿using System.Data.SqlClient;
using LvivCompany.Bookstore.DataAccess;
using LvivCompany.Bookstore.DataAccess.IRepo;
using LvivCompany.Bookstore.Entities;
using LvivCompany.Bookstore.Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using LvivCompany.Bookstore.Web.ViewModels;
using System.Linq;
using LvivCompany.Bookstore.Web.Mapper;

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

            services.AddScoped<RoleManager<IdentityRole<long>>, RoleManager<IdentityRole<long>>>();

            var config = new AutoMapper.MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<RegisterViewModel, User>();
                 cfg.CreateMap<EditProfileViewModel, User>()
                         .ForMember(d => d.PasswordHash, o => o.Ignore())
                     .ForMember(d => d.PhoneNumberConfirmed, o => o.Ignore())
                     .ForMember(d => d.Photo, o => o.Ignore())
                     .ForMember(d => d.SecurityStamp, o => o.Ignore())
                     .ForMember(d => d.UserName, o => o.Ignore())
                     .ForMember(d => d.AccessFailedCount, o => o.Ignore())
                     .ForMember(d => d.ConcurrencyStamp, o => o.Ignore());

                    cfg.CreateMap<User, EditProfileViewModel>();
                }
               );
            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);

            services.AddMvc();
            services.AddAutoMapper();
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // TODO: Remove when dabase will be ready
            services.AddTransient<IRepo<Book>, BookRepository>();
            services.AddTransient<IRepo<Author>, AuthorRepository>();
            services.AddTransient<IRepo<Category>, CategoryRepository>();
            services.AddTransient<IRepo<Order>, OrderRepository>();
            services.AddTransient<IRepo<OrderDetail>, OrderDetailRepository>();
            services.AddTransient<IRepo<Publisher>, PublisherRepository>();
            services.AddTransient<IRepo<Status>, StatusRepository>();

            services.AddTransient<IMapper<Book,BookDetailViewModel>, BookDetailMapper>();
            services.AddTransient<IMapper<Book, BookInfo>, BookMapper>();
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
