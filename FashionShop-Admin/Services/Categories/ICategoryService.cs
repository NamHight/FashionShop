using FashionShop.Models;
using FashionShop.Models.views.CategoryViewModels;
using FashionShop.Models.views.ProductViewModels;
namespace FashionShop.Services.Categories;

public interface ICategoryService
{

    Task<List<Category>> GetAllAsync(bool trackChanges);
    Task<CategoryViewModel> GetPageLinkAsync(int page, int pageSize, string nameSearch, string typeCategory, bool trackChanges);
    Task AddNewCategory(Category category);
    Task<bool> CheckSlug(string slug);
    Task CreateCategoryAsync(Category category);
    Task<bool> DeleteAsync(long id, bool trackChanges);
    Task UpdateStatusAsync(long id, string status, bool trackChanges);
    Task<Category?> GetByIdAsync(long id, bool trackChanges);
    Task<long> FindByNameAsync(string newCategoryName);
    Task<bool> UpdateCategoryAsync(int id, UpdateCategoryViewModel abc, bool trackChanges);
    Task<int> GetCategoryCountAsync();


}