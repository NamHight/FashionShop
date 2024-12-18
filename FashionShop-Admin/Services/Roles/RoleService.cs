using FashionShop.Models;
using FashionShop.Models.views;
using FashionShop.Models.views.RoleViewModels;
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

    public async Task<RoleViewModel> GetAllPaginateAsync(int page, int limit, bool trackChanges)
    {
        var role = await _managerRepository.Role.GetAllPaginateAsync(page, limit, trackChanges);
        var count = await _managerRepository.Role.CountAsync();
        var result = new RoleViewModel
        {
            Roles = role,
            PagingInfo = new PagingInfo
            {
                TotalItems = count,
                CurrentPage = page,
                ItemsPerPage = limit,
            }
        };
        return result;
    }

    public async Task<bool> CreateAsync(CreateRoleViewModel role)
    {
        try
        {
            var newRole = new Role
            {
                RoleName = role.RoleName,
                Description = role.Description,
            };
            await _managerRepository.Role.CreateAsync(newRole);
            var result = await _managerRepository.SaveAsyncAndNumRowEffect();
            if (result.Equals(0))
            {
                return false;
            }
            return true;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<CreateRoleViewModel> GetByIdAsync(int id, bool trackChanges)
    {
        try
        {
            var role = await _managerRepository.Role.FindByIdAsync(id, trackChanges);
            if (role is null)
            {
                return null;
            }
            var result = new CreateRoleViewModel
            {
                RoleName = role.RoleName,
                Description = role.Description,
                Status = role.Status
            };
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> CheckUniqueName(string name, bool trackChanges)
    {
        try
        {
            var result = await _managerRepository.Role.FindByNameAsync(name, trackChanges);
            if (result is null)
            {
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(int id, CreateRoleViewModel role,bool trackChanges)
    {
        try
        {
            var currentRole = await _managerRepository.Role.FindByIdAsync(id, trackChanges);
            if (currentRole is null)
            {
                return false;
            }

            currentRole.RoleName = role.RoleName;
            currentRole.Description = role.Description;
            currentRole.Status = role.Status;
            _managerRepository.Role.Edit(currentRole);
            var result = await _managerRepository.SaveAsyncAndNumRowEffect();
            if (result.Equals(0))
            {
                return false;
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

    public async Task<bool> DeleteAsync(int id,bool trackChanges)
    {
        var role = await _managerRepository.Role.FindByIdAsync(id, trackChanges);
        _managerRepository.Role.Remove(role);
        var result = await _managerRepository.SaveAsyncAndNumRowEffect();
        if (result.Equals(0))
        {
            return false;
        }
        return true;
    }
}