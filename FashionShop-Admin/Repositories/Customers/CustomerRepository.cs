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

        public async Task<int> CountAsync()
        {
            return await _context.Customers.Where(cus => cus.Status.Equals("active")).AsNoTracking().CountAsync();
        }

        public async Task<int> CountStatusAsync(string status)
        {
            return await _context.Customers.Where(cus => cus.Status.Equals(status)).AsNoTracking().CountAsync();
        }

        public async Task<int> CountCustomerByMonthInYearAsync(DateTime date)
        {
            return await _context.Customers.Where(cus => cus.CreatedAt.Value.Date.Equals(date.Date)).AsNoTracking().CountAsync();
        }
        public async Task<int> CountCustomerAsync()
        {
            return await _context.Customers.CountAsync();
        }
    }
}
