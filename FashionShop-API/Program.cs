using FashionShop_API.Extensions;
using FashionShop_API.Mappers;
using FashionShop_API.Options;
using FashionShop_API.Services.ServiceLogger;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.ConfigureGetConnection(builder.Configuration);
builder.Services.ConfigureResponseCaching();
builder.Services.ConfigureCors();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerManager();
builder.Services.ConfigureServiceCaching();
builder.Services.ConfigureRedisConnection(builder.Configuration);
builder.Services.ConfigureFilter();
builder.Services.ConfigureSendEmail();
builder.Services.Configure<GmailOption>(builder.Configuration.GetSection(GmailOption.GmailOptionKey));
builder.Services.AddControllers(
    options =>
    {
        
    }).AddNewtonsoftJson();
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
builder.Services.AddDistributedMemoryCache();
builder.Services.ConfigureSession();


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
app.ConfigureException(logger);
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");
app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
