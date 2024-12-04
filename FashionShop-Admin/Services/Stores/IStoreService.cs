using FashionShop.Models;

namespace FashionShop.Services.Stores;

public interface IStoreService
{
    Task<IEnumerable<Store>> GetAllAsync(bool trackChanges);
}