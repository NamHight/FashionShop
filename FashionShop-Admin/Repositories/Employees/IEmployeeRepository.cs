using FashionShop.Models;

namespace FashionShop.Repositories.Employees;

public interface IEmployeeRepository
{
    Task<Employee?> LoginAsync(string email, bool trackChanges);
    Task<IEnumerable<Employee>> GetAllAsync(bool trackChanges);
    Task<Employee?> GetByIdAsync(long id, bool trackChanges);
    Task<bool> CheckUniqueEmail(string email, bool trackChanges);
    void CreateAsync(Employee employee);
}