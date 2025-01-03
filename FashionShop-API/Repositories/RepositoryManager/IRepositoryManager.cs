using FashionShop_API.Repositories.Categories;
using FashionShop_API.Repositories.Customers;
using FashionShop_API.Repositories.Favorites;

namespace FashionShop_API.Repositories.RepositoryManager;

public interface IRepositoryManager
{
    IRepositoryCategory Category { get; }
    IRepositoryCustomer Customer { get; }
    IRepositoryFavorites Favorite { get; }

    Task SaveChanges();
}