using FashionShop_API.Repositories.Products;
using FashionShop_API.Repositories.Categories;
using FashionShop_API.Repositories.Contacts;
using FashionShop_API.Repositories.Customers;
using FashionShop_API.Repositories.Favorites;
using FashionShop_API.Repositories.Reviews;
using FashionShop_API.Repositories.Promotions;


namespace FashionShop_API.Repositories;

public interface IRepositoryManager
{
    IRepositoryCategory Category { get; }
    IRepositoryCustomer Customer { get; }
    IRepositoryContact Contact { get; }
    IRepositoryFavorites Favorite { get; }
    IRepositoryProduct Product { get; }
	IRepositoryReviews Review { get; }

	Task SaveChanges();
    IRepositoryPromotion Promotion { get; }
    Task SaveChanges();
}