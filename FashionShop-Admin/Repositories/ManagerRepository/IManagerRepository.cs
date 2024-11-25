using FashionShop.Repositories.Categories;

namespace FashionShop.Repositories.ManagerRepo;

public interface IManagerRepository
{
    ICategoryRepository Category { get; }
    
    Task SaveAsync();
}