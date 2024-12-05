using FashionShop.Models;

namespace FashionShop.Services.Categories;

public interface ICategoryService
{

    Task<List<Category>> GetAllAsync(bool trackChanges);
    Task<List<Category>> GetPageLinkAsync(int page, int pageSize, bool trackChanges);
    Task AddNewCategory(Category category);
    Task<bool> CheckSlug(string slug);
    Task CreateCategoryAsync(Category category);
    Task<bool> DeleteAsync(long id, bool trackChanges);
    Task UpdateStatusAsync(long id, string status, bool trackChanges);


    Task<long> FindByNameAsync(string newCategoryName);

    Task<long> FindByNameAsync(string newCategoryName);

}