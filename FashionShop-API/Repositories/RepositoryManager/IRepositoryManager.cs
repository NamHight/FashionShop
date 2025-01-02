using FashionShop_API.Repositories.Categories;
using FashionShop_API.Repositories.Contacts;
using FashionShop_API.Repositories.Customers;
using FashionShop_API.Repositories.Promotions;

namespace FashionShop_API.Repositories.RepositoryManager;

public interface IRepositoryManager
{
    IRepositoryCategory Category { get; }
    IRepositoryCustomer Customer { get; }
    IRepositoryContact Contact { get; }
    IRepositoryPromotion Promotion { get; }
    Task SaveChanges();
}