using FashionShop_API.Services.Chating;

namespace FashionShop_API.Extensions;

public static class ApplicationService
{
    public static void ConfigureApplicationService(this WebApplication app)
    {
        app.MapHub<ServiceChat>("/chat");
    }
}