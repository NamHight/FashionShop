using FashionShop.Repositories.ManagerRepository;
using FashionShop.Services.Categories;
using FashionShop.Services.Contacts;
using FashionShop.Services.Employees;
using FashionShop.Services.Roles;
using FashionShop.Services.Stores;
using FashionShop.Services.Customers;
using FashionShop.Services.Products;
using FashionShop.Services.Reviews;
using FashionShop.Services.Orders;
using FashionShop.Services.OrdersDetails;
namespace FashionShop.Services.ManagerService;

public class ManagerService : IManagerService
{
    private readonly Lazy<ICategoryService> _categoryService;
    private readonly Lazy<IContactService> _contactService;
    private readonly Lazy<IEmployeeService> _employeeService;
    private readonly Lazy<IRoleService> _roleService;
    private readonly Lazy<IStoreService> _storeService;
    private readonly Lazy<IProductService> _productService;
    private readonly Lazy<IReviewService> _reviewService;
    private readonly Lazy<ICustomerService> _customerService;
    private readonly Lazy<IOrdersService> _ordersService;
    private readonly Lazy<IOrdersDetailsService> _ordersDetailsService;

    public ManagerService(IManagerRepository managerRepository,IWebHostEnvironment hostingEnvironment)
    {
        _categoryService = new Lazy<ICategoryService>(() => new CategoryService(managerRepository));
        _contactService = new Lazy<IContactService> (()=> new ContactService(managerRepository));
        _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(managerRepository,hostingEnvironment));
        _roleService = new Lazy<IRoleService>(() => new RoleService(managerRepository));
        _storeService = new Lazy<IStoreService>(() => new StoreService(managerRepository));
        _productService = new Lazy<IProductService>(() => new ProductService(managerRepository));
        _reviewService = new Lazy<IReviewService>(()=> new ReviewService(managerRepository));
        _customerService = new Lazy<ICustomerService>(()=> new CustomerService(managerRepository));
        _ordersService = new Lazy<IOrdersService>(() => new OrdersService(managerRepository));
        _ordersDetailsService = new Lazy<IOrdersDetailsService>(() => new OrdersDetailsService(managerRepository));
    }
    
    public ICategoryService Category => _categoryService.Value;
    public IProductService Product => _productService.Value;
    public IContactService Contact => _contactService.Value;
    public IReviewService Review => _reviewService.Value;
    public ICustomerService Customer => _customerService.Value;
    public IEmployeeService Employee => _employeeService.Value;
    public IStoreService Store => _storeService.Value;
    public IRoleService Role => _roleService.Value;
    public IOrdersService Orders => _ordersService.Value;
    public IOrdersDetailsService OrdersDetails => _ordersDetailsService.Value;

}