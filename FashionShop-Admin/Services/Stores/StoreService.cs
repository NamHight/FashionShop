using FashionShop.Models;
using FashionShop.Models.views;
using FashionShop.Models.views.StoreViewModels;
using FashionShop.Repositories.ManagerRepository;

namespace FashionShop.Services.Stores;

public class StoreService : IStoreService
{
    private readonly IManagerRepository _managerRepository;

    public StoreService(IManagerRepository managerRepository)
    {
        _managerRepository = managerRepository;
    }
    
    public async Task<IEnumerable<Store>> GetAllAsync(bool trackChanges)
    {
        var stores = await _managerRepository.Store.GetAllAsync(trackChanges);
        return stores;
    }

    public async Task<StoreViewModel> GetAllPaginateAsync(int page, int limit, bool trackChanges)
    {
        var stores = await _managerRepository.Store.GetAllPaginateAsync(page, limit, trackChanges);
        var count = await _managerRepository.Store.CountAsync();
        var result = new StoreViewModel
        {
            Stores = stores,
            PagingInfo = new PagingInfo
            {
                TotalItems = count,
                CurrentPage = page,
                ItemsPerPage = limit
            }
        };
        return result;
    }
}