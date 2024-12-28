using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FashionShop_API.Context;
using FashionShop_API.Filters;
using FashionShop_API.Models;
using FashionShop_API.Repositories.RepositoryManager;
using FashionShop_API.Services.Caching;
using FashionShop_API.Services.Emails;
using FashionShop_API.Services.ServiceLogger;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace FashionShop_API.Extensions;

public static class ServicesExtension
{
    public static void ConfigureGetConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MyDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "CacheRedis_";
        });
    }
    public static void ConfigureAuthentication(this IServiceCollection services,IConfiguration configuration)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
        var jwtSetting = configuration.GetSection("Jwt");
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 

            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSetting["Issuer"],
                    ValidAudience = jwtSetting["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting["SecretKey"]!))
                };
            });
        services.AddAuthorization();
    }

    public static void ConfigureRedisConnection(this IServiceCollection services,IConfiguration configuration)
        => services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var settingConnection = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis")!,true);
            return ConnectionMultiplexer.Connect(settingConnection);
        });
    public static void ConfigureResponseCaching(this IServiceCollection services)
    {
        services.AddResponseCaching();
    }
    public static void ConfigureCors(this IServiceCollection services)
        => services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder 
            => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
        });

    public static void ConfigureRepositoryManager(this IServiceCollection service)
        => service.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceCaching(this IServiceCollection service)
    {
        service.AddScoped<IServiceCacheRedis, ServiceCacheRedis>();
    }
    public static void ConfigureServiceManager(this IServiceCollection service)
        => service.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureLoggerManager(this IServiceCollection service)
        => service.AddSingleton<ILoggerManager, LoggerManager>();
    public static void ConfigureFilter(this IServiceCollection service)
    {
        service.AddScoped<ValidationFilter>();
    }

    public static void ConfigureSendEmail(this IServiceCollection services) =>
        services.AddScoped<IServiceEmail, ServiceEmail>();
}