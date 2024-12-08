using FashionShop.Models;
using System.Threading.Tasks;

namespace FashionShop.Repositories.Products
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(bool trackChanges);
        Task<Product?> GetByIdAsync(long id, bool trackChanges);
        void AddNewProductAsync(Product product);

        Task<bool> CheckSlugAsync(string slug);

        Task UpdateCategoryIdAsync(long newCategoryID, long idProduct, bool trackChanges);

        Task UpdateStatusAsync(string newData, long idProduct, bool trackChanges);

        void UpdateProduct(Product product);

        void DeleteProduct(Product product);
    }
}
