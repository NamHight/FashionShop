using FashionShop.Models;
using FashionShop.Repositories.ManagerRepo;

namespace FashionShop.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IManagerRepository _managerRepository;
        public ProductService(IManagerRepository managerRepository) => _managerRepository = managerRepository;

        public async Task<List<Category>> GetAllAsync(bool trackChanges)
        {
            var products = await _managerRepository.Product.GetAllAsync(trackChanges);
            return products;
        }
    }
}
