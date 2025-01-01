using FashionShop_API.Repositories.Categories;
using FashionShop_API.Repositories.Customers;

namespace FashionShop_API.Repositories.RepositoryManager;

public interface IRepositoryManager
{
    IRepositoryCategory Category { get; }
    IRepositoryCustomer Customer { get; }

    Task SaveChanges();
}