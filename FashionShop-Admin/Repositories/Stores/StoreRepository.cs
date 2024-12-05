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
}