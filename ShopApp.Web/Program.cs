using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopApp.Business.Services;
using ShopApp.Data;
using ShopApp.Data.GenericRepository;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ShopContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnectionString"), options => options.EnableRetryOnFailure()));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserAddressService, UserAddressService>();
builder.Services.AddScoped<ILookupService, LookupService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IBlogCategoryService, BlogCategoryService>();
builder.Services.AddScoped<ITodoCategoryService, TodoCategoryService>();
builder.Services.AddScoped<ITodoStatusService, TodoStatusService>();
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddScoped<IEmailSenderService, SmtpEmailSenderService>(i =>
     new SmtpEmailSenderService(
         builder.Configuration["EmailSender:Host"],
         builder.Configuration.GetValue<int>("EmailSender:Port"),
         builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
         builder.Configuration["EmailSender:UserName"],
         builder.Configuration["EmailSender:Password"])
    );

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

app.UseStaticFiles(); // wwwroot

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "admin",
        areaName: "Admin",
        pattern: "admin/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();