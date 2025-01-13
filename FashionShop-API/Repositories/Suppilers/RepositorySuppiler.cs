using FashionShop_API.Context;
using FashionShop_API.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
namespace FashionShop_API.Repositories.Suppilers
{
    public class RepositorySuppiler: RepositoryBase<Supplier>, IRepositorySuppiler
    {
        public RepositorySuppiler(MyDbContext context) : base(context)
        {
        }
        public async Task<List<Supplier>> GetAllAsync(bool trackChanges)
        {
            var suppliers = await FindAll(trackChanges).OrderByDescending(e => e.SupplierId).ToListAsync();
            return suppliers;
        }
    }
}
