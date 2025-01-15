using FashionShop_API.Context;
using FashionShop_API.Models;
using FashionShop_API.Repositories.Shared;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Repositories.Promotions
{
    public class RepositoryPromotion : RepositoryBase<Promotion>, IRepositoryPromotion
    {
        public RepositoryPromotion(MyDbContext context):base(context) { }
        
        public async Task<IEnumerable<Promotion>> GetAllPromotionAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();
        }

        public async Task<Promotion> GetByIdAsync(long id, bool trackChanges)
        {
            return await FindByCondition(item => item.PromotionId == id, trackChanges).FirstAsync();
        }

        public async Task<PagedListAsync<Promotion>> GetPaginateAsync(int page, int limit)
        {
            return await PagedListAsync<Promotion>.ToPagedListAsync(_context.Promotions.OrderByDescending(item => item.CreatedAt).AsQueryable(), page, limit);
        }
    }
}
