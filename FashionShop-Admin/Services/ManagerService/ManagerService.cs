using FashionShop.Repositories.ManagerRepo;
using FashionShop.Services.Categories;

namespace FashionShop.Services.ManagerService;

public class ManagerService : IManagerService
{
    private readonly Lazy<ICategoryService> _categoryService;
    
    public ManagerService(IManagerRepository managerRepository)
    {
        _categoryService = new Lazy<ICategoryService>(() => new CategoryService(managerRepository));
    }
    
    public ICategoryService Category => _categoryService.Value;

}