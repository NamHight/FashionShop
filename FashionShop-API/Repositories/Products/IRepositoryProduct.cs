using FashionShop_API.Models;
using System.Threading.Tasks;

namespace FashionShop_API.Repositories.Products
{
    public interface IRepositoryProduct
    {
        Task<IEnumerable<Product>?> GetListProductByCategoryId(long categoryId, bool trackChanges);
        Task<List<Product>> GetAllAsync(bool trackChanges);
        Task<Product?> GetByIdAsync(long id, bool trackChanges);
        Task<Product> GetProductDetailsAsync(string categorySlug, string productSlug);

	}
}
