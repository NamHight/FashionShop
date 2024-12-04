using FashionShop.Models;
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
}