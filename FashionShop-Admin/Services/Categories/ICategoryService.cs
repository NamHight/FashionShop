using FashionShop.Models;
namespace FashionShop.Services.Categories;

public interface ICategoryService
{

    Task<List<Category>> GetAllAsync(bool trackChanges);
    Task<CategoryViewModel> GetPageLinkAsync(int page, int pageSize, string nameSearch, bool trackChanges);
    Task AddNewCategory(Category category);
    Task<bool> CheckSlug(string slug);
    Task CreateCategoryAsync(Category category);
    Task<bool> DeleteAsync(long id, bool trackChanges);
    Task UpdateStatusAsync(long id, string status, bool trackChanges);


    Task<long> FindByNameAsync(string newCategoryName);


}