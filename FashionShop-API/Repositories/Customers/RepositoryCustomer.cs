using FashionShop_API.Context;
using FashionShop_API.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Repositories.Customers;

public class RepositoryCustomer : RepositoryBase<Customer>, IRepositoryCustomer
{
    public RepositoryCustomer(MyDbContext context) : base(context)
    {
    }
    
    public async Task CreateAsync(Customer customer)
    {
        await Create(customer);
    }

    public async Task<bool> CheckEmailExistsAsync(string email, bool trackChanges)
    {
        var checkEmail = await FindByCondition(c => c.Email.Equals(email), trackChanges).FirstOrDefaultAsync();
        return checkEmail is not null;
    }

    public async Task<Customer?> GetCustomerByEmailAsync(string email, bool trackChanges)
    {
        var customer = await FindByCondition(c => c.Email.Equals(email), trackChanges).FirstOrDefaultAsync();
        return customer;
    }

    public async Task<Customer?> GetCustomerByIdAsync(long id, bool trackChanges)
    {
        var customer = await FindByCondition(c => c.CustomerId.Equals(id), trackChanges).FirstOrDefaultAsync();
        return customer;
    }
    public void UpdateCustomer(Customer customer) => Update(customer);
    public async Task<Customer?> GetCustomerByGoogleIdAsync(string id, bool trackChanges)
    {
        var customer = await FindByCondition(c => c.GoogleId.Equals(id), trackChanges).FirstOrDefaultAsync();
        return customer;
    }
}