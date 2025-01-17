using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Extensions;
using FashionShop_API.Helper;
using FashionShop_API.Mappers;
using FashionShop_API.Options;
using FashionShop_API.Services.Chating;
using FashionShop_API.Services.ServiceLogger;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureWebsocket();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.ConfigureGetConnection(builder.Configuration);
builder.Services.ConfigureResponseCaching();
builder.Services.AddSingleton<ServiceShareData>();
builder.Services.ConfigureCors();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.AddAuthentication("JwtBearerDefaults.AuthenticationScheme")
  .AddCookie("JwtBearerDefaults.AuthenticationScheme", options =>
  {
      options.LoginPath = "/Account/Login";
      options.AccessDeniedPath = "/Account/AccessDenied";
  });
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerManager();
builder.Services.ConfigureSession();
builder.Services.ConfigureRedisConnection(builder.Configuration);
builder.Services.ConfigureServiceCaching();
builder.Services.ConfigureFilter();
builder.Services.ConfigureSendEmail();
builder.Services.Configure<GmailOption>(builder.Configuration.GetSection(GmailOption.GmailOptionKey));
builder.Services.Configure<GoogleOption>(builder.Configuration.GetSection("Google"));
builder.Services.AddControllers(
    options =>
    {
    }).AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
              Reference  = new OpenApiReference
              {
                  Type = ReferenceType.SecurityScheme,
                  Id = "Bearer"
              },
              Name = "Bearer",
            },
            new List<string>()
        }
    });
});
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(x =>
    new PaypalClient(
        builder.Configuration["PaypalOptions:ClientId"],
        builder.Configuration["PaypalOptions:ClientSecret"],
        builder.Configuration["PaypalOptions:Mode"]
    )
);

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.ConfigureException(logger);
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.MapHub<ServiceChat>("/Chat");
app.UseCors("CorsPolicy");
app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllers();
app.Run();
