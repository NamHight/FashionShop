using FashionShop.Context;
using FashionShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Repositories.Customers
{
    public class CustomerRepository : GenericRepo<Customer>, ICustomerRepository
    {
        public CustomerRepository(MyDbContext context) : base(context) { }
        public async Task<List<Customer>> GetAllAsync(bool trackChanges)
        {
            var customers = await FindAll(trackChanges).ToListAsync();
            return customers;
        }
      
    }
}
