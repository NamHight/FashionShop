using FashionShop.Models;

namespace FashionShop.Repositories.Roles;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAllAsync(bool trackChanges);
}