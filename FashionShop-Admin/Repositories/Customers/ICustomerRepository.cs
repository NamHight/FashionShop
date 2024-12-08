using FashionShop.Models;

namespace FashionShop.Repositories.Customers
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync(bool trackChanges);

        Task<int> CountAsync();
        Task<int> CountStatusAsync(string status);
        Task<int> CountCustomerByMonthInYearAsync(DateTime date);
    }
}
