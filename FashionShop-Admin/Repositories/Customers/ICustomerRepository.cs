using FashionShop.Models;

namespace FashionShop.Repositories.Customers
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync(bool trackChanges);
    }
}
