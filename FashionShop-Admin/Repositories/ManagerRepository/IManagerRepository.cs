using FashionShop.Repositories.Categories;
using FashionShop.Repositories.Contacts;
using FashionShop.Repositories.Products;

namespace FashionShop.Repositories.ManagerRepo;

public interface IManagerRepository
{
    ICategoryRepository Category { get; }

    IProductRepository Product { get; }


    IContactRepository Contact { get; } 
    
    Task SaveAsync();
}