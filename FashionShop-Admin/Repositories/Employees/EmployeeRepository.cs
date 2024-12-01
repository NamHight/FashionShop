using FashionShop.Context;
using FashionShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Repositories.Employees;

public class EmployeeRepository : GenericRepo<Employee> , IEmployeeRepository
{
    public EmployeeRepository(MyDbContext context) : base(context)
    {
    }

    public async Task<Employee?> LoginAsync(string email,bool trackChanges)
    {
        var employee = await FindById(employee => employee.Email.Equals(email), trackChanges).FirstOrDefaultAsync();
        return employee;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(bool trackChanges)
    {
        var employee = await FindAll(trackChanges).Include(employee => employee.Role).Include(employee => employee.Store).ToListAsync();
        return employee;
    }

    public void Create(Employee employee)
    {
       Create(employee);
    }
}