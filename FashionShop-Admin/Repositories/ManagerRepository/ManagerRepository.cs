using FashionShop.Context;
using FashionShop.Repositories.Categories;
using FashionShop.Repositories.Contacts;

namespace FashionShop.Repositories.ManagerRepo;

public class ManagerRepository : IManagerRepository
{
    private readonly MyDbContext _context;
    private readonly Lazy<ICategoryRepository> _categoryRepository;
    private readonly Lazy<IContactRepository> _contactRepository;
    
    public ManagerRepository(MyDbContext context)
    {
        _context = context;
        _categoryRepository = new Lazy<ICategoryRepository> ( () => new CategoryRepository(context) );
        _contactRepository = new Lazy<IContactRepository> ( () => new ContactRepository(context) );
        
    }
    
    public ICategoryRepository Category => _categoryRepository.Value;
    public IContactRepository Contact => _contactRepository.Value;
    
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}