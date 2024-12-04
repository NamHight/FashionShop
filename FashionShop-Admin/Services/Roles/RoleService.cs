using FashionShop.Models;
using FashionShop.Repositories.ManagerRepository;

namespace FashionShop.Services.Roles;

public class RoleService : IRoleService
{
    private readonly IManagerRepository _managerRepository;

    public RoleService(IManagerRepository _managerRepository)
    {
        this._managerRepository = _managerRepository;
    }
    
    public async Task<IEnumerable<Role>> GetAllAsync(bool trackChanges)
    {
        var roles = await _managerRepository.Role.GetAllAsync(trackChanges);
        return roles;
    }
}