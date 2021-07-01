using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopApp.Business.Services;
using ShopApp.Data;
using ShopApp.Data.GenericRepository;
using System;

namespace ShopApp.WebUI
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShopContext>(options => options.UseSqlServer(_configuration.GetConnectionString("AppConnectionString")));           

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".ShopApp.Security.Cookie",
                    SameSite = SameSiteMode.Strict
                };
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IUserService, UserService>(); 

            services.AddScoped<IEmailSenderService, SmtpEmailSenderService>(i =>
                 new SmtpEmailSenderService(
                     _configuration["EmailSender:Host"],
                     _configuration.GetValue<int>("EmailSender:Port"),
                     _configuration.GetValue<bool>("EmailSender:EnableSSL"),
                     _configuration["EmailSender:UserName"],
                     _configuration["EmailSender:Password"])
                );

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration, ICartService cartService)
        {
            app.UseStaticFiles(); // wwwroot
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                  name: "orders",
                  pattern: "orders",
                  defaults: new { controller = "Cart", action = "GetOrders" }
              );


                endpoints.MapControllerRoute(
                  name: "checkout",
                  pattern: "checkout",
                  defaults: new { controller = "Cart", action = "Checkout" }
              );

                endpoints.MapControllerRoute(
                    name: "cart",
                    pattern: "cart",
                    defaults: new { controller = "Cart", action = "Index" }
                );

                // localhost/search    
                endpoints.MapControllerRoute(
                    name: "search",
                    pattern: "search",
                    defaults: new { controller = "Shop", action = "search" }
                );

                endpoints.MapControllerRoute(
                    name: "productdetails",
                    pattern: "{url}",
                    defaults: new { controller = "Shop", action = "details" }
                );

                endpoints.MapControllerRoute(
                    name: "products",
                    pattern: "products/{category?}",
                    defaults: new { controller = "Shop", action = "list" }
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapAreaControllerRoute(
                    areaName: "Admin",
                    name: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
               );
            });
        }
    }
}
