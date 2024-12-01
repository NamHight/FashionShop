using FashionShop.Repositories.ManagerRepository;
using FashionShop.Services.Categories;
using FashionShop.Services.Contacts;
using FashionShop.Services.Employees;
using FashionShop.Services.Roles;
using FashionShop.Services.Stores;

namespace FashionShop.Services.ManagerService;

public class ManagerService : IManagerService
{
    private readonly Lazy<ICategoryService> _categoryService;
    private readonly Lazy<IContactService> _contactService;
    private readonly Lazy<IEmployeeService> _employeeService;
    private readonly Lazy<IRoleService> _roleService;
    private readonly Lazy<IStoreService> _storeService;
    public ManagerService(IManagerRepository managerRepository)
    {
        _categoryService = new Lazy<ICategoryService>(() => new CategoryService(managerRepository));
        _contactService = new Lazy<IContactService> (()=> new ContactService(managerRepository));
        _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(managerRepository));
        _roleService = new Lazy<IRoleService>(() => new RoleService(managerRepository));
        _storeService = new Lazy<IStoreService>(() => new StoreService(managerRepository));
    }
    
    public ICategoryService Category => _categoryService.Value;
    public IContactService Contact => _contactService.Value;

    public IEmployeeService Employee => _employeeService.Value;
    public IStoreService Store => _storeService.Value;

    public IRoleService Role => _roleService.Value;

}