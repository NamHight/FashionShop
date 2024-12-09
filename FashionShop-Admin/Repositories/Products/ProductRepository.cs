using FashionShop.Context;
using FashionShop.Models;
using Microsoft.EntityFrameworkCore;


namespace FashionShop.Repositories.Products
{
    public class ProductRepository : GenericRepo<Product>, IProductRepository
    {
        public ProductRepository(MyDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetAllAsync(bool trackChanges)
        {
            var products = await FindAll(trackChanges).OrderByDescending(e => e.ProductId).ToListAsync();
            return products;
        }

        public async Task<Product?> GetByIdAsync(long id, bool trackChanges)
        {
            var product = await FindById(item => item.ProductId == id, trackChanges).FirstOrDefaultAsync();
            return product;
        }

        public void AddNewProductAsync(Product product)
        {
            Create(product);
        }
        public async Task<bool> CheckSlugAsync(string slug)
        {
            var result = await _context.Products.Where(item => item.Slug.Equals(slug)).FirstOrDefaultAsync();
            if (result != null) return true;
            return false;
        }

        public async Task UpdateCategoryIdAsync(long newCategoryID, long idProduct, bool trackChanges)
        {
            var result = await FindById(item => item.ProductId == idProduct, trackChanges).FirstOrDefaultAsync();
            if (result != null)
            {
                result.CategoryId = newCategoryID;
                // trackChanges = true thì chỉ cần gán lại thuộc tính cần thay đổi, k cần update
                if (!trackChanges)
                {
                    Update(result); // nếu trackChanges = false thì phải tự update
                }
            }
        }

        public async Task UpdateStatusAsync(string newData, long idProduct, bool trackChanges)
        {
            var result = await FindById(item => item.ProductId == idProduct, trackChanges).FirstOrDefaultAsync();
            result.Status = newData;
            if (!trackChanges)
            {
                Update(result); // nếu trackChanges = false thì phải tự update
            }
        }

        public void UpdateProduct(Product product)
        {
            Update(product);
        }
        public async Task<List<Product>> GetPageLinkAsync(int page, int pageSize, string nameSearch, bool trackChanges)
        {
            if (!string.IsNullOrEmpty(nameSearch))
            {
                return await PageLinkAsync(page, pageSize, trackChanges).Where(item => item.ProductName.Contains(nameSearch)).ToListAsync();
            }
            return await PageLinkAsync(page, pageSize, trackChanges).ToListAsync();
        }
        public async Task<int> GetCountAsync(string nameSearch, bool trackChanges)
        {
            if (!string.IsNullOrEmpty(nameSearch))
            {
                return await FindById(item => item.ProductName.Contains(nameSearch), trackChanges).CountAsync();
            }
            return await FindAll(trackChanges).CountAsync();
        }

        public void DeleteProduct(Product product)
        {
            Delete(product);
        }

        public async Task<int> CountByDateAsync(DateTime date, bool trackChanges)
        {
            var count = await FindById(product => product.CreatedAt.Value.Date.Equals(date.Date), trackChanges)
                .CountAsync();
            return count;
        }
    }
}
