using FashionShop_API.Services.Authenticates;
using FashionShop_API.Services.Categories;
using FashionShop_API.Services.Contacts;
using FashionShop_API.Services.Customers;
using FashionShop_API.Services.Products;
using FashionShop_API.Services.Favorites;
using FashionShop_API.Services.Googles;
using FashionShop_API.Services.WebsiteInfos;
using FashionShop_API.Services.Reviews;

namespace FashionShop_API.Services.ServiceManager;

public interface IServiceManager
{
     IServiceCategory Category { get; }
     IServiceAuthenticate Authenticate { get; }
     IServiceCustomer Customer { get; }
     IServiceProduct Product { get; }
     IServiceFavorites Favorite { get; }
     IServiceContact Contact { get; }
	 IServiceReviews Review { get; }
     IServiceWebsiteInfo WebsiteInfo { get; }
     IServiceGoogle Google { get; }
}