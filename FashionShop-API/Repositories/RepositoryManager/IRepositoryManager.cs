using FashionShop_API.Repositories.Products;
using FashionShop_API.Repositories.Categories;
using FashionShop_API.Repositories.Customers;



namespace FashionShop_API.Repositories;

public interface IRepositoryManager
{
    IRepositoryCategory Category { get; }
    IRepositoryCustomer Customer { get; }
    IRepositoryProduct Product { get; }

    Task SaveChanges();
}