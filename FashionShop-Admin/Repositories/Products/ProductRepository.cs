using FashionShop.Context;
using FashionShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FashionShop.Repositories.Products
{
    public class ProductRepository : GenericRepo<Product>,IProductRepository
    {
        public ProductRepository(MyDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetAllAsync(bool trackChanges)
        {
            var products = await FindAll(trackChanges).ToListAsync();
            return products;
        }

        public async Task<Product?> GetByIdAsync(long id, bool trackChanges)
        {
            var category = await FindById(item => item.CategoryId == id, trackChanges).FirstOrDefaultAsync();
            return category;
        }

        public  void AddNewProduct(Product product)
        {
            Create(product);
        }
        public async Task<bool> CheckSlug(string slug)
        {
            var result = await _context.Products.Where(item=> item.Slug.Equals(slug)).FirstOrDefaultAsync();
            if (result!=null) return true;
            return false;   
        }
    }
}
