using FashionShop.Repositories.ManagerRepo;
using FashionShop.Services.Categories;
using FashionShop.Services.Contacts;

namespace FashionShop.Services.ManagerService;

public class ManagerService : IManagerService
{
    private readonly Lazy<ICategoryService> _categoryService;
    private readonly Lazy<IContactService> _contactService;
    
    public ManagerService(IManagerRepository managerRepository)
    {
        _categoryService = new Lazy<ICategoryService>(() => new CategoryService(managerRepository));
        _contactService = new Lazy<IContactService> (()=> new ContactService(managerRepository));
    }
    
    public ICategoryService Category => _categoryService.Value;
    public IContactService Contact => _contactService.Value;

}