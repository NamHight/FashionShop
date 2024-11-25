using FashionShop.Models;

namespace FashionShop.Repositories.Products
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(bool trackChanges);
        Task<Product?> GetByIdAsync(long id, bool trackChanges);
    }
}
