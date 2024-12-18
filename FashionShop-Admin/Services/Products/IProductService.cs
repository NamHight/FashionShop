using FashionShop.Models;
using FashionShop.Models.views.DashboardViewModel;
using FashionShop.Models.views.ProductViewModels;

namespace FashionShop.Services.Products
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync(bool trackChanges);

        Task AddNewProduct(Product product);

        Task<bool> CheckSlug(string slug);

        Task<bool> UpdateCategoryId(long newCategoryID, long idProduct, bool v);

        Task<string> HandleUploadImage(IWebHostEnvironment hostingEnvironment, IFormFile Banner);

        Task<bool> UpdateStatus(string newData, long idProduct, bool trackChanges);

        Task<Product?> GetByIdAsync(long id, bool trackChanges);

        Task<bool> UpdateProductAsync(int id, UpdateProductViewModel abc, string image);

        Task<ProductViewModel> GetPageLinkAsync(string nameSearch, int page, int pageSize, bool trackChanges);

        Task<bool> DeleteProductAsync(int productId);
        Task<int> GetProductCountAsync();
    }
}
