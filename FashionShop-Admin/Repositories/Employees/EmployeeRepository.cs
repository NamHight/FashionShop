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
        var employee = await FindById(employee => employee.Email.Equals(email), trackChanges).Include(employee => employee.Role).FirstOrDefaultAsync();
        return employee;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(bool trackChanges)
    {
        var employee = await FindAll(trackChanges)
            .Include(employee => employee.Role)
            .Include(employee => employee.Store)
            .OrderByDescending(item=>item.CreatedAt)
            .Take(10)
            .ToListAsync();
        return employee;
    }

    public void CreateAsync(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee), "Employee cannot be null.");
        }
       Create(employee);
    }
}