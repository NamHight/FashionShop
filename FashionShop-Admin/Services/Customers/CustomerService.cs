using FashionShop.Models;
using FashionShop.Repositories.ManagerRepo;

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
            var customers = await _managerRepository.Customer.GetAllAsync(trackChanges);
            return customers;
        }
    }
}
