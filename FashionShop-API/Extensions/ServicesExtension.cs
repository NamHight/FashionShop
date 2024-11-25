using FashionShop_API.Context;
using FashionShop_API.Repositories.RepositoryManager;
using FashionShop_API.Services.ServiceLogger;
using FashionShop_API.Services.ServiceManager;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Extensions;

public static class ServicesExtension
{
    public static void ConfigureMySql(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<MyDbContext>(options => options.UseMySql(configuration.GetConnectionString("DefaultConnection"),ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));

    public static void ConfigureCors(this IServiceCollection services)
        => services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder 
            => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });

    public static void ConfigureRepositoryManager(this IServiceCollection service)
        => service.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection service)
        => service.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureLoggerManager(this IServiceCollection service)
        => service.AddSingleton<ILoggerManager, LoggerManager>();


}