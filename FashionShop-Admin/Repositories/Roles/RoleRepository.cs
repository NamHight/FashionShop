using FashionShop.Context;
using FashionShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Repositories.Roles;

public class RoleRepository : GenericRepo<Role>, IRoleRepository
{
    public RoleRepository(MyDbContext context) : base(context)
    {
    }


    public async Task<IEnumerable<Role>> GetAllAsync(bool trackChanges)
    {
        var roles = await FindAll(trackChanges).ToListAsync();
        return roles;
    }

    public async Task<IEnumerable<Role>> GetAllPaginateAsync(int page, int limit, bool trackChanges)
    {
        var roles = await FindAll(trackChanges)
            .OrderBy(role => role.RoleId)
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        return roles;
    }

    public async Task<int> CountAsync()
    {
        return await Count();
    }

    public async Task CreateAsync(Role role)
    {
        Create(role);
    }

    public async Task<Role> FindByNameAsync(string name, bool trackChanges)
    {
        var role = await FindById(role => role.RoleName.Equals(name), trackChanges).FirstOrDefaultAsync();
        return role;
    }

    public async Task<Role> FindByIdAsync(int id, bool trackChanges)
    {
        var role = await FindById(role => role.RoleId.Equals(id), trackChanges).FirstOrDefaultAsync();
        return role;
    }

    public void Edit(Role role)
    {
        Update(role);
    }

    public void Remove(Role role)
    {
        Delete(role);
    }
}