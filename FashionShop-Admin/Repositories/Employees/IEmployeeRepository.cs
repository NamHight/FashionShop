using FashionShop.Models;

namespace FashionShop.Repositories.Employees;

public interface IEmployeeRepository
{
    Task<Employee?> LoginAsync(string email, bool trackChanges);
    Task<IEnumerable<Employee>> GetAllAsync(bool trackChanges);

    void Create(Employee employee);
}