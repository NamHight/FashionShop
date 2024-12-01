using FashionShop.Context;
using FashionShop.Repositories.Categories;
using FashionShop.Repositories.Contacts;
using FashionShop.Repositories.Products;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Repositories.ManagerRepo;

public class ManagerRepository : IManagerRepository
{
    private readonly MyDbContext _context;
    private readonly Lazy<ICategoryRepository> _categoryRepository;
    private readonly Lazy<IContactRepository> _contactRepository;
    private readonly Lazy<IProductRepository> _productRepository;

    public ManagerRepository(MyDbContext context)
    {
        _context = context;
        _categoryRepository = new Lazy<ICategoryRepository> ( () => new CategoryRepository(context) );
        _contactRepository = new Lazy<IContactRepository> ( () => new ContactRepository(context) );
        _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(context));

    }
    
    public ICategoryRepository Category => _categoryRepository.Value;
    public IProductRepository Product => _productRepository.Value;
    public IContactRepository Contact => _contactRepository.Value;

    public async Task<int> SaveAsyncAndNumRowEffect()
    {
        try
        {
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            Console.WriteLine("Lỗi cạnh tranh dữ liệu: " + ex.Message);
            return 0;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine("Lỗi cập nhật dữ liệu: " + ex.Message);
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi không xác định: " + ex.Message);
            return 0;
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}