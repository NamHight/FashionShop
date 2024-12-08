using FashionShop.Models;

namespace FashionShop.Repositories.Employees;

public interface IEmployeeRepository
{
    Task<Employee?> LoginAsync(string email, bool trackChanges);
    Task<IEnumerable<Employee>> GetAllAsync(bool trackChanges);
    Task<Employee?> GetByIdWithRoleAndStoreAsync(long id, bool trackChanges);
    Task<bool> CheckUniqueEmail(string email, bool trackChanges);
    void CreateAsync(Employee employee);
    Task<Employee?> GetById(long id, bool trackChanges);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(Employee employee);
    Task<IEnumerable<Employee>> GetPaginateAllAsync(int page, int limit, bool trackChanges);
    Task<int> CountAsync();
}