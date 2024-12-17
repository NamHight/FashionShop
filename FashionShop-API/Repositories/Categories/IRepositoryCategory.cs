using FashionShop_API.Models;
using FashionShop_API.Repositories.Shared;

namespace FashionShop_API.Repositories.Categories;

public interface IRepositoryCategory
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
    Task<Category?> GetCategoryByIdAsync(long id, bool trackChanges);
    
    Task<PagedListAsync<Category>> GetAllPaginatedAsync(int page, int limit);
    Task CreateAsync(Category category);
    Task<bool> CheckCategorySlugExistsAsync(string slug);
}