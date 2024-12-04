using FashionShop.Repositories.ManagerRepo;
using FashionShop.Services.Categories;
using FashionShop.Services.Contacts;
using FashionShop.Services.Customers;
using FashionShop.Services.Products;
using FashionShop.Services.Reviews;
namespace FashionShop.Services.ManagerService;

public class ManagerService : IManagerService
{
    private readonly Lazy<ICategoryService> _categoryService;
    private readonly Lazy<IContactService> _contactService;
    private readonly Lazy<IProductService> _productService;
    private readonly Lazy<IReviewService> _reviewService;
    private readonly Lazy<ICustomerService> _customerService;

    public ManagerService(IManagerRepository managerRepository)
    {
        _categoryService = new Lazy<ICategoryService>(() => new CategoryService(managerRepository));
        _contactService = new Lazy<IContactService> (()=> new ContactService(managerRepository));
        _productService = new Lazy<IProductService>(() => new ProductService(managerRepository));
        _reviewService = new Lazy<IReviewService>(()=> new ReviewService(managerRepository));
        _customerService = new Lazy<ICustomerService>(()=> new CustomerService(managerRepository));
    }
    
    public ICategoryService Category => _categoryService.Value;
    public IProductService Product => _productService.Value;
    public IContactService Contact => _contactService.Value;
    public IReviewService Review => _reviewService.Value;
    public ICustomerService Customer => _customerService.Value;

}