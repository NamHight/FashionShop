using FashionShop_API.Repositories.Categories;

namespace FashionShop_API.Repositories.RepositoryManager;

public interface IRepositoryManager
{
    IRepositoryCategory Category { get; }
    
    Task SaveChanges();
}