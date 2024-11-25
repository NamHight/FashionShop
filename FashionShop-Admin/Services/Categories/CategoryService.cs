using FashionShop.Models;
using FashionShop.Repositories.ManagerRepo;

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
    
}