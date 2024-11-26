using FashionShop.Repositories.Categories;
using FashionShop.Repositories.Contacts;

namespace FashionShop.Repositories.ManagerRepo;

public interface IManagerRepository
{
    ICategoryRepository Category { get; }

    IContactRepository Contact { get; } 
    
    Task SaveAsync();
}