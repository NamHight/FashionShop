using FashionShop_API.Context;
using FashionShop_API.Repositories.Categories;
using FashionShop_API.Repositories.Products;
using FashionShop_API.Repositories.Customers;

namespace FashionShop_API.Repositories.RepositoryManager;

public class RepositoryManager : IRepositoryManager
{
    private readonly MyDbContext _context;
    private Lazy<IRepositoryCategory> _category;
    private Lazy<IRepositoryCustomer> _customer;
    private Lazy<IRepositoryProduct> _product;
    public RepositoryManager (MyDbContext context)
    {
        _context = context;
        _customer = new Lazy<IRepositoryCustomer>(() => new RepositoryCustomer(context));
        _category = new Lazy<IRepositoryCategory>( () => new RepositoryCategory(context) );
        _product = new Lazy<IRepositoryProduct>(() => new RepositoryProduct(context));
    }

    public IRepositoryCategory Category => _category.Value;
    public IRepositoryCustomer Customer => _customer.Value;

    public IRepositoryProduct Product => _product.Value;

    public async Task SaveChanges() => await _context.SaveChangesAsync();
}