using FashionShop.Models;

namespace FashionShop.Repositories.Categories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(bool trackChanges);
    Task<Category?> GetByIdAsync(long id, bool trackChanges);
    void AddNewCategory(Category category);
    Task<bool> CheckSlug(string slug);
    void CreateCategoryAsync(Category category);
    void UpdateStatus(Category category);
    Task<bool> DeleteAsync(long id, bool trackChanges);
    Task<List<Category>> GetPageLinkAsync(int page, int pageSize, bool trackChanges);
}