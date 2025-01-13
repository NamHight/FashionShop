using FashionShop_API.Repositories.Products;
using FashionShop_API.Repositories.Categories;
using FashionShop_API.Repositories.Contacts;
using FashionShop_API.Repositories.Customers;
using FashionShop_API.Repositories.Favorites;
using FashionShop_API.Repositories.Reviews;
using FashionShop_API.Repositories.WebsiteInfos;
using FashionShop_API.Repositories.Promotions;
using FashionShop_API.Repositories.Articles;
using FashionShop_API.Repositories.Orders;
using FashionShop_API.Repositories.Suppilers;

using FashionShop_API.Repositories.Views;

namespace FashionShop_API.Repositories;

public interface IRepositoryManager
{
    IRepositorySuppiler Suppiler { get; }
    IRepositoryCategory Category { get; }
    IRepositoryCustomer Customer { get; }
    IRepositoryContact Contact { get; }
    IRepositoryFavorites Favorite { get; }
    IRepositoryProduct Product { get; }
	IRepositoryReviews Review { get; }
    IRepositoryPromotion Promotion { get; }
    IRepositoryWebsiteInfo WebsiteInfo { get; }
    IRepositoryArticle Article { get; }
    IRepositoryOrders Orders { get; }
    IRepositoryViews Views { get; }
    Task SaveChanges();
    Task<bool> SaveChangesAsync();
}