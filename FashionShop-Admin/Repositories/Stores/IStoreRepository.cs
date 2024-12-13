using FashionShop.Models;

namespace FashionShop.Repositories.Stores;

public interface IStoreRepository
{
    Task<IEnumerable<Store>> GetAllAsync(bool trackChanges);
    Task<IEnumerable<Store>> GetAllPaginateAsync(int page, int limit, bool trackChanges);
    Task<int> CountAsync();
}