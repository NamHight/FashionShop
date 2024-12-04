using FashionShop.Context;
using FashionShop.Repositories.Categories;
using FashionShop.Repositories.Contacts;
using FashionShop.Repositories.Employees;
using FashionShop.Repositories.Roles;
using FashionShop.Repositories.Stores;
using FashionShop.Repositories.Customers;
using FashionShop.Repositories.Products;
using FashionShop.Repositories.Reviews;

namespace FashionShop.Repositories.ManagerRepository;

public class ManagerRepository : IManagerRepository
{
    private readonly MyDbContext _context;
    private readonly Lazy<ICategoryRepository> _categoryRepository;
    private readonly Lazy<IContactRepository> _contactRepository;
    private readonly Lazy<IEmployeeRepository> _employeeRepository;
    private readonly Lazy<IRoleRepository> _roleRepository;
    private readonly Lazy<IStoreRepository> _storeRepository;
    
    private readonly Lazy<IProductRepository> _productRepository;
    private readonly Lazy<IReviewRepository> _reviewRepository;
    private readonly Lazy<ICustomerRepository> _customerRepository;
    public ManagerRepository(MyDbContext context)
    {
        _context = context;
        _categoryRepository = new Lazy<ICategoryRepository> ( () => new CategoryRepository(context) );
        _contactRepository = new Lazy<IContactRepository> ( () => new ContactRepository(context) );
        _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(context));
        _roleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(context));
        _storeRepository = new Lazy<IStoreRepository>(() => new StoreRepository(context));
        _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(context));
        _reviewRepository = new Lazy<IReviewRepository>(() => new ReviewRepository(context));
        _customerRepository = new Lazy<ICustomerRepository> ( () => new CustomerRepository(context));
    }
    
    public ICategoryRepository Category => _categoryRepository.Value;
    public IProductRepository Product => _productRepository.Value;
    public IContactRepository Contact => _contactRepository.Value;
    public IEmployeeRepository Employee => _employeeRepository.Value;
    public IRoleRepository Role => _roleRepository.Value;
    public IStoreRepository Store => _storeRepository.Value;
    
    public IReviewRepository Review => _reviewRepository.Value;
    public ICustomerRepository Customer => _customerRepository.Value;
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}