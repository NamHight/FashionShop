using FashionShop.Models;
using FashionShop.Models.views.StoreViewModels;

namespace FashionShop.Services.Stores;

public interface IStoreService
{
    Task<IEnumerable<Store>> GetAllAsync(bool trackChanges);

    Task<StoreViewModel> GetAllPaginateAsync(int page, int limit, bool trackChanges);
}