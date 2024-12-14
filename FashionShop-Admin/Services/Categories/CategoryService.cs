using FashionShop.Models;
using FashionShop.Repositories.ManagerRepository;
using FashionShop.Models.views;
namespace FashionShop.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly IManagerRepository _managerRepository;
    public CategoryService(IManagerRepository managerRepository) => _managerRepository = managerRepository;

    public async Task<List<Category>> GetAllAsync(bool trackChanges)
    {   
        var categories = await _managerRepository.Category.GetAllAsync(trackChanges);
        return categories;
    }

    public async Task CreateCategoryAsync(Category category)
    { 
        _managerRepository.Category.CreateCategoryAsync(category);
        await _managerRepository.SaveAsync();
    }
    public async Task AddNewCategory(Category category)
    {
        _managerRepository.Category.AddNewCategory(category);
        await _managerRepository.SaveAsync();
    }

    public async Task<bool> CheckSlug(string slug)
    {
        var result = await _managerRepository.Category.CheckSlug(slug);
        if (result) return true;
        return false;
    }
    public async Task<bool> DeleteAsync(long id, bool trackChanges)
    {
        var category = await _managerRepository.Category.DeleteAsync(id, trackChanges);
        return category;
    }
    public async Task UpdateStatusAsync(long id, string status, bool trackChanges)
    {
        var category = await _managerRepository.Category.GetByIdAsync(id, trackChanges);
        if (category != null)
        {
            category.Status = status;
            await _managerRepository.SaveAsync();
        }


    }
    public async Task<CategoryViewModel> GetPageLinkAsync(int page, int pageSize, string nameSearch, bool trackChanges)
    {
        var categoryPages = await _managerRepository.Category.GetPageLinkAsync(page, pageSize, nameSearch, trackChanges);
        var categoryCounts = await _managerRepository.Category.GetCountAsync(nameSearch, trackChanges);
        var result = new CategoryViewModel
        {
            Categories = categoryPages,
            NameSearch = nameSearch,
            PagingInfo = new PagingInfo
            {
                TotalItems = categoryCounts,
                ItemsPerPage = pageSize,
                CurrentPage = page,
            }
        };
        return result;
    }
    public async Task<long> FindByNameAsync(string newCategoryName)
    {
        return await _managerRepository.Category.FindByNameAsync(newCategoryName);
    }
}