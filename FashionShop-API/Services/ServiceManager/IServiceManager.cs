using FashionShop_API.Services.Categories;
using FashionShop_API.Services.ServiceAuthenticate;

namespace FashionShop_API.Services.ServiceManager;

public interface IServiceManager
{
     IServiceCategory Category { get; }
     IServiceAuthenticate Authenticate { get; }
}