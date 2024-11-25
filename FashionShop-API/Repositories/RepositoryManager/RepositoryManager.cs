using FashionShop_API.Context;
using FashionShop_API.Repositories.Categories;

namespace FashionShop_API.Repositories.RepositoryManager;

public class RepositoryManager : IRepositoryManager
{
    private readonly MyDbContext _context;
    private Lazy<IRepositoryCategory> _category;
    
    public RepositoryManager (MyDbContext context)
    {
        _context = context;
        _category = new Lazy<IRepositoryCategory>( () => new RepositoryCategory(context) );
    }

    public IRepositoryCategory Category => _category.Value;
    
    public async Task SaveChanges() => await _context.SaveChangesAsync();
}