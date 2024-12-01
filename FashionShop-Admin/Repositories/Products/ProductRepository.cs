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

        public async Task UpdateCategoryId(long newCategoryID, long idProduct, bool trackChanges)
        {
            var result = await FindById(item => item.ProductId == idProduct, trackChanges).FirstOrDefaultAsync(); 
            if (result!=null)
            {
                result.CategoryId = newCategoryID;
                // trackChanges = true thì chỉ cần gán lại thuộc tính cần thay đổi, k cần update
                if (!trackChanges)
                {
                    Update(result); // nếu trackChanges = false thì phải tự update
                }
            }
        }
    }
}
