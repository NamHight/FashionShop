using FashionShop.Context;
using FashionShop.Repositories.Categories;

namespace FashionShop.Repositories.ManagerRepo;

public class ManagerRepository : IManagerRepository
{
    private readonly MyDbContext _context;
    private readonly Lazy<ICategoryRepository> _categoryRepository;
    
    public ManagerRepository(MyDbContext context)
    {
        _context = context;
        _categoryRepository = new Lazy<ICategoryRepository> ( () => new CategoryRepository(context) );
        
    }
    
    public ICategoryRepository Category => _categoryRepository.Value;
    
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}