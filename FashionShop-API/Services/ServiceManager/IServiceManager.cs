using FashionShop_API.Services.Authenticates;
using FashionShop_API.Services.Categories;
using FashionShop_API.Services.Contacts;
using FashionShop_API.Services.Customers;
using FashionShop_API.Services.Products;
using FashionShop_API.Services.Favorites;
using FashionShop_API.Services.WebsiteInfos;

namespace FashionShop_API.Services.ServiceManager;

public interface IServiceManager
{
     IServiceCategory Category { get; }
     IServiceAuthenticate Authenticate { get; }
     IServiceCustomer Customer { get; }
     IServiceProduct Product { get; }
     IServiceFavorites Favorite { get; }
     IServiceContact Contact { get; }
     IServiceWebsiteInfo WebsiteInfo { get; }
}