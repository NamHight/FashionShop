using FashionShop.Models;
using FashionShop.Repositories.ManagerRepository;

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
    public async Task<long> FindByNameAsync(string newCategoryName)
    {
        return await _managerRepository.Category.FindByNameAsync(newCategoryName);
    }
}