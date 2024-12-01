using FashionShop.Models;
using FashionShop.Repositories.ManagerRepo;

namespace FashionShop.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IManagerRepository _managerRepository;
        public ProductService(IManagerRepository managerRepository) => _managerRepository = managerRepository;

        public async Task<List<Product>> GetAllAsync(bool trackChanges)
        {
            var products = await _managerRepository.Product.GetAllAsync(trackChanges);
            return products;
        }

        public async Task AddNewProduct(Product product)
        {
            _managerRepository.Product.AddNewProduct(product);
            await _managerRepository.SaveAsync();
        }

        public async Task<bool> CheckSlug(string slug)
        {
            var result = await _managerRepository.Product.CheckSlug(slug);
            if (result) return true;
            return false;
        }

        public async Task<bool> UpdateCategoryId(long newCategoryID, long idProduct, bool trackChanges)
        {
            await _managerRepository.Product.UpdateCategoryId(newCategoryID, idProduct, trackChanges);
            int numRowEffect = await _managerRepository.SaveAsyncAndNumRowEffect();
            Console.WriteLine($"so dong bi anh huong la {numRowEffect}");
            if (numRowEffect > 0) return true;
            return false;
        }
    } 
}
