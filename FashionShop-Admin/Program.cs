using FashionShop.Extensions;
using FashionShop.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews( );
builder.Services.ConfigureCors();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureEmailService(builder.Configuration);
builder.Services.ConfigureManagerRepository();
builder.Services.ConfigureManagerService();
builder.Services.AddSession(option =>
{
    option.Cookie.HttpOnly = true; 
    option.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.ConfigureAuthenticate();
builder.Services.AddAuthorization();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(30);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name:"Login",
    pattern:"login",
    defaults:new {Controller="Authenticate",Action="Login"}
    );
app.MapControllerRoute(
    "analysisRoute",
    "{controller}/analysis",
    new {Action="Analysis"}
);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}"
);


app.Run();