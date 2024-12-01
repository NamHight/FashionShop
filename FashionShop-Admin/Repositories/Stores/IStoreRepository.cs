using FashionShop.Models;

namespace FashionShop.Repositories.Stores;

public interface IStoreRepository
{
    Task<IEnumerable<Store>> GetAllAsync(bool trackChanges);
}