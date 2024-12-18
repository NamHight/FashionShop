using System.Collections;
using FashionShop.Models;
using FashionShop.Models.views;
using FashionShop.Models.views.RoleViewModels;

namespace FashionShop.Services.Roles;

public interface IRoleService
{
    Task<IEnumerable<Role>> GetAllAsync(bool trackChanges);

    Task<RoleViewModel> GetAllPaginateAsync(int page, int limit, bool trackChanges);
    Task<bool> CreateAsync(CreateRoleViewModel role);
    Task<CreateRoleViewModel> GetByIdAsync(int id, bool trackChanges);
    Task<bool> CheckUniqueName(string name, bool trackChanges);
    
    Task<bool> UpdateAsync(int id, CreateRoleViewModel role,bool trackChanges);
    
    Task<bool> DeleteAsync(int id,bool trackChanges);
}