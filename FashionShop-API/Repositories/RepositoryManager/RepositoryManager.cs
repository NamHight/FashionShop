using AutoMapper;
using FashionShop_API.Context;
using FashionShop_API.Repositories.Categories;
using FashionShop_API.Repositories.Contacts;
using FashionShop_API.Repositories.Customers;

namespace FashionShop_API.Repositories.RepositoryManager;

public class RepositoryManager : IRepositoryManager
{
    private readonly MyDbContext _context;
    private Lazy<IRepositoryCategory> _category;
    private Lazy<IRepositoryCustomer> _customer;
    private Lazy<IRepositoryContact> _contact;
    public RepositoryManager (MyDbContext context,IMapper _mapper)
    {
        _context = context;
        _customer = new Lazy<IRepositoryCustomer>(() => new RepositoryCustomer(context));
        _category = new Lazy<IRepositoryCategory>( () => new RepositoryCategory(context,_mapper));
        _contact = new Lazy<IRepositoryContact>(() => new RepositoryContact(context));
    }

    public IRepositoryCategory Category => _category.Value;
    public IRepositoryCustomer Customer => _customer.Value;
    public IRepositoryContact Contact => _contact.Value;
    public async Task SaveChanges() => await _context.SaveChangesAsync();
}