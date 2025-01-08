
using FashionShop_API.Models;

namespace FashionShop_API.Services.Products
{
    public interface IServiceProduct
    {
        Task<List<Product>> GetAllAsync(bool trackChanges);
        Task<Product?> FindProductByIdAsync( long id, bool trackChanges);
    }
}
