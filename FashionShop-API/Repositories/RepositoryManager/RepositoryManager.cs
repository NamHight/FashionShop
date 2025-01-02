using FashionShop_API.Context;
using FashionShop_API.Repositories.Categories;
using FashionShop_API.Repositories.Contacts;
using FashionShop_API.Repositories.Customers;
using FashionShop_API.Repositories.Promotions;

namespace FashionShop_API.Repositories.RepositoryManager;

public class RepositoryManager : IRepositoryManager
{
    private readonly MyDbContext _context;
    private Lazy<IRepositoryCategory> _category;
    private Lazy<IRepositoryCustomer> _customer;
    private Lazy<IRepositoryContact> _contact;
    private Lazy<IRepositoryPromotion> _promotion;
    public RepositoryManager (MyDbContext context)
    {
        _context = context;
        _customer = new Lazy<IRepositoryCustomer>(() => new RepositoryCustomer(context));
        _category = new Lazy<IRepositoryCategory>( () => new RepositoryCategory(context));
        _contact = new Lazy<IRepositoryContact>(() => new RepositoryContact(context));
        _promotion = new Lazy<IRepositoryPromotion>(() => new RepositoryPromotion(context));
    }

    public IRepositoryCategory Category => _category.Value;
    public IRepositoryCustomer Customer => _customer.Value;
    public IRepositoryContact Contact => _contact.Value;
    public IRepositoryPromotion Promotion => _promotion.Value;
    public async Task SaveChanges() => await _context.SaveChangesAsync();
}