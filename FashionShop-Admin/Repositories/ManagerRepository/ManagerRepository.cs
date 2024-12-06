using FashionShop.Context;
using FashionShop.Repositories.Categories;
using FashionShop.Repositories.Contacts;
using FashionShop.Repositories.Employees;
using FashionShop.Repositories.Roles;
using FashionShop.Repositories.Stores;
using FashionShop.Repositories.Customers;
using FashionShop.Repositories.Products;
using FashionShop.Repositories.Reviews;
using Microsoft.EntityFrameworkCore;
using FashionShop.Repositories.Orders;
using FashionShop.Repositories.OrdersDetails;


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
    private readonly Lazy<IOrdersRepository> _ordersRepository;
    private readonly Lazy<IOrdersDetailsRepository> _ordersDetailsRepository;
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
        _ordersRepository = new Lazy<IOrdersRepository>(() => new OrdersRepository(context));
        _ordersDetailsRepository = new Lazy<IOrdersDetailsRepository>(() => new OrdersDetailsRepository(context));
    }
    
    public ICategoryRepository Category => _categoryRepository.Value;
    public IProductRepository Product => _productRepository.Value;
    public IContactRepository Contact => _contactRepository.Value;
    public IEmployeeRepository Employee => _employeeRepository.Value;
    public IRoleRepository Role => _roleRepository.Value;
    public IStoreRepository Store => _storeRepository.Value;
    public IOrdersRepository Orders => _ordersRepository.Value;
    public IOrdersDetailsRepository OrdersDetails => _ordersDetailsRepository.Value;
    public IReviewRepository Review => _reviewRepository.Value;
    public ICustomerRepository Customer => _customerRepository.Value;
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task<int> SaveAsyncAndNumRowEffect()
    {
        try
        {
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            Console.WriteLine("Loi canh tranh du lieu: " + ex.Message);
            return 0;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine("Loi cap nhat du lieu: " + ex.Message);
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Loi khong xac đinh: " + ex.Message);
            return 0;
        }
    }
}