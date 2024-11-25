using FashionShop.Context;
using FashionShop.Repositories.Categories;
using FashionShop.Repositories.Products;

namespace FashionShop.Repositories.ManagerRepo;

public class ManagerRepository : IManagerRepository
{
    private readonly MyDbContext _context;
    private readonly Lazy<ICategoryRepository> _categoryRepository;
    private readonly Lazy<IProductRepository> _productRepository;

    public ManagerRepository(MyDbContext context)
    {
        _context = context;
        _categoryRepository = new Lazy<ICategoryRepository> ( () => new CategoryRepository(context) );
        _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(context));

    }
    
    public ICategoryRepository Category => _categoryRepository.Value;
    public IProductRepository Product => _productRepository.Value;

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}