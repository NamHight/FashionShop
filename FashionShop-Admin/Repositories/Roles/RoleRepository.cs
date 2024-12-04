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
    
}