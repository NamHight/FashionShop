using FashionShop.Models;

namespace FashionShop.Repositories.Categories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(bool trackChanges);
    Task<Category?> GetByIdAsync(long id, bool trackChanges);

    void CreateCategoryAsync(Category category);

    Task<long> FindByNameAsync(string newCategoryName);
}