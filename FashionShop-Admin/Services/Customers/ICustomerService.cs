using FashionShop.Models;

namespace FashionShop.Services.Customers
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllAsync(bool trackChanges);
        
        Task<int> CountAsync();
    }
}
