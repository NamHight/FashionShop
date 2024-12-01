using FashionShop.Context;
using FashionShop.Models;
using FashionShop.Repositories.ManagerRepository;
using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<MyDbContext>(options => options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
            ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));

    public static void ConfigureAuthenticate(this IServiceCollection service)
        => service.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.AccessDeniedPath = "/login";
            });
    
    
    public static void ConfigureManagerRepository(this IServiceCollection services) =>
        services.AddScoped<IManagerRepository, ManagerRepository>();
    
    public static void ConfigureManagerService(this IServiceCollection services) =>
        services.AddScoped<IManagerService, ManagerService>();
    
   
    
}