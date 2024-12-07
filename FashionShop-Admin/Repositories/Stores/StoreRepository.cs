using FashionShop.Context;
using FashionShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Repositories.Stores;

public class StoreRepository : GenericRepo<Store>, IStoreRepository
{
    public StoreRepository(MyDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Store>> GetAllAsync(bool trackChanges)
    {
        var stores = await FindAll(trackChanges).ToListAsync();
        return stores;
    }

    public async Task<IEnumerable<Store>> GetAllPaginateAsync(int page, int limit, bool trackChanges)
    {
        var stores = await FindAll(trackChanges)
            .Include(store => store.Employees)
            .OrderByDescending(store => store.CreatedAt)
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();
        
        return stores;
    }

    public async Task<int> CountAsync()
    {
        return await Count();
    }
}