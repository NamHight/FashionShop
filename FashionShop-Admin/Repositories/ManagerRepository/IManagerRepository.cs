using FashionShop.Repositories.Categories;
using FashionShop.Repositories.Contacts;
using FashionShop.Repositories.Employees;
using FashionShop.Repositories.Roles;
using FashionShop.Repositories.Stores;
using FashionShop.Repositories.Customers;
using FashionShop.Repositories.Products;
using FashionShop.Repositories.Reviews;

namespace FashionShop.Repositories.ManagerRepository;

public interface IManagerRepository
{
    ICategoryRepository Category { get; }
    IProductRepository Product { get; }
    IContactRepository Contact { get; } 
    
    IEmployeeRepository Employee { get; }
    
    IRoleRepository Role { get; }
    
    IStoreRepository Store { get; }
    
    IReviewRepository Review { get; }
    ICustomerRepository Customer { get; }
    Task SaveAsync();
}