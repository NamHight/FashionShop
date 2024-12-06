using System.Data.Common;
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
        var employees = await FindAll(trackChanges).ToListAsync();
        return employees;
    }

    public async Task<IEnumerable<Employee>> GetPaginateAllAsync(int page, int limit,bool trackChanges)
    {
        var employees = await FindAll(trackChanges)
            .Include(employee => employee.Role)
            .Include(employee => employee.Store)
            .OrderByDescending(item=>item.CreatedAt)
            .Skip((page - 1 )* limit)
            .Take(10)
            .ToListAsync();
        return employees;
    }

    public async Task<int> CountAsync()
    {
        return await Count();
    }

    public async Task<Employee?> GetByIdWithRoleAndStoreAsync(long id, bool trackChanges)
    {
        var employee = await FindById(employee => employee.EmployeeId.Equals(id), trackChanges).Include(employee => employee.Role).Include(empl => empl.Store).FirstOrDefaultAsync();
        return employee;
    }

    public async Task<bool> CheckUniqueEmail(string email, bool trackChanges)
    {
        var employee = await FindById(employee => employee.Email != null && employee.Email.Equals(email), trackChanges)
            .FirstOrDefaultAsync();
        return employee != null; 
    }


    public void CreateAsync(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee), "Employee cannot be null.");
        }
       Create(employee);
    }

    public async Task<Employee?> GetById(long id, bool trackChanges)
    {
        var employee = await FindById(employee => employee.EmployeeId.Equals(id), trackChanges).FirstOrDefaultAsync();
        return employee;
    }

    public async Task UpdateAsync(Employee employee)
    {
        try
        {
            Update(employee);
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine("Update failed " + e);
        }
        catch (DbException e)
        {
            Console.WriteLine("Update failed " +e);
        }
        catch (Exception e)
        {
            Console.WriteLine("Update failed " +e);
        }
       
    }
    public async Task DeleteAsync(Employee employee)
    {
        throw new NotImplementedException();
    }
}