using FashionShop_API.Models;
using System.Threading.Tasks;

namespace FashionShop_API.Repositories.Products
{
    public interface IRepositoryProduct
    {
        Task<List<Product>> GetAllAsync(bool trackChanges);
        Task<Product?> GetByIdAsync(long id, bool trackChanges);
    }
}
