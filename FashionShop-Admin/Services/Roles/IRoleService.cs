using FashionShop.Models;

namespace FashionShop.Services.Roles;

public interface IRoleService
{
    Task<IEnumerable<Role>> GetAllAsync(bool trackChanges);
}