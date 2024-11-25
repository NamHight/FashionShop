using FashionShop_API.Models;

namespace FashionShop_API.Repositories.Categories;

public interface IRepositoryCategory
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
    Task<Category?> GetCategoryByIdAsync(long id, bool trackChanges);
}