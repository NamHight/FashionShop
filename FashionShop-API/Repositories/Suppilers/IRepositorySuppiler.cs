using FashionShop_API.Models;
namespace FashionShop_API.Repositories.Suppilers
{
    public interface IRepositorySuppiler
    {
        Task<List<Supplier>> GetAllAsync(bool trackChanges);
    }
}
