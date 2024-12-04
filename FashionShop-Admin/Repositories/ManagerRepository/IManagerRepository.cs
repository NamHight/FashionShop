using FashionShop.Repositories.Categories;
using FashionShop.Repositories.Contacts;
using FashionShop.Repositories.Customers;
using FashionShop.Repositories.Products;
using FashionShop.Repositories.Reviews;

namespace FashionShop.Repositories.ManagerRepo;

public interface IManagerRepository
{
    ICategoryRepository Category { get; }
    IProductRepository Product { get; }
    IContactRepository Contact { get; } 
    IReviewRepository Review { get; }
    ICustomerRepository Customer { get; }
    Task SaveAsync();
}