using System.Collections;
using FashionShop.Models;

namespace FashionShop.Repositories.Roles;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAllAsync(bool trackChanges);

    Task<IEnumerable<Role>> GetAllPaginateAsync(int page, int limit, bool trackChanges);
    
    Task<int> CountAsync();
    
    Task CreateAsync(Role role);
    
    Task<Role> FindByNameAsync(string name, bool trackChanges);
    
    Task<Role> FindByIdAsync(int id, bool trackChanges);
    
    void Edit(Role role);
    
    void Remove(Role role);
}