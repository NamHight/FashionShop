using FashionShop.Models;
using System.Threading.Tasks;

namespace FashionShop.Repositories.Products
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(bool trackChanges);
        Task<Product?> GetByIdAsync(long id, bool trackChanges);
        void AddNewProduct(Product product);

        Task<bool> CheckSlug(string slug);

        Task UpdateCategoryId(long newCategoryID, long idProduct, bool trackChanges);
    }
}
