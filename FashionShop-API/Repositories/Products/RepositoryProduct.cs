
using FashionShop_API.Context;
using FashionShop_API.Models;
using FashionShop_API.Repositories;
using Microsoft.EntityFrameworkCore;


namespace FashionShop_API.Repositories.Products
{
    public class RepositoryProduct : RepositoryBase<Product>, IRepositoryProduct
    {
        public RepositoryProduct(MyDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetAllAsync(bool trackChanges)
        {
            var products = await FindAll(trackChanges).OrderByDescending(e => e.ProductId).ToListAsync();
            return products;
        }

        public async Task<Product?> GetByIdAsync(long id, bool trackChanges)
        {
            var product = await FindByCondition(item => item.ProductId == id, trackChanges).FirstOrDefaultAsync();
            return product;
        }

    }
}
