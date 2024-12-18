using FashionShop.Models;
using FashionShop.Repositories.ManagerRepository;

namespace FashionShop.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly IManagerRepository _managerRepository;

        public CustomerService(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }
        public async Task<List<Customer>> GetAllAsync(bool trackChanges)
        {
            try
            {
                var customers = await _managerRepository.Customer.GetAllAsync(trackChanges);
                return customers;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                var count = await _managerRepository.Customer.CountAsync();
                return count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public async Task<int> GetCustomerCountAsync()
        {
            return await _managerRepository.Customer.CountCustomerAsync();
        }
    }
}
