using FashionShop.Models;

namespace FashionShop.Repositories.Categories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(bool trackChanges);
}