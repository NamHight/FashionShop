using FashionShop_API.Models;

namespace FashionShop_API.Repositories.Customers;

public interface IRepositoryCustomer
{
    Task CreateAsync(Customer customer);
    Task<bool> CheckEmailExistsAsync(string email, bool trackChanges);
    Task<Customer?> GetCustomerByEmailAsync(string email, bool trackChanges);
    Task<Customer?> GetCustomerByIdAsync(long id, bool trackChanges);
    void UpdateCustomer(Customer customer);
    Task<Customer?> GetCustomerByGoogleIdAsync(string id, bool trackChanges);
}