using FashionShop.Models;
using FashionShop.Models.views;
using FashionShop.Repositories.ManagerRepository;

namespace FashionShop.Services.Employees;

public class EmployeeService : IEmployeeService
{
    private readonly IManagerRepository _managerRepository;

    public EmployeeService(IManagerRepository managerRepository)
    {
        _managerRepository = managerRepository;
    }
    
    public async Task<Employee?> LoginAsync(string email, string password, bool trackChanges)
    {
        var employee = await _managerRepository.Employee.LoginAsync(email, trackChanges);
        if (employee == null && employee?.Password != null && VerifyPassword(employee.Password, password))
        {
            return null;
        }
        return employee;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(bool trackChanges)
    {
        var employees = await _managerRepository.Employee.GetAllAsync(trackChanges);
        return employees;
    }

    public async Task CreateAsync(CreateEmployeeViewModel employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee), "Employee data cannot be null.");
        }
        var newEmployee = new Employee
        {
            Email = employee.Email,
            Password = HashPassword(employee.Password),
            RoleId = employee.RoleId,
            StoreId = employee.StoreId,
            Avatar = employee.Avatar,
            EmployeeName = employee.EmployeeName,
            Gender = employee.Gender,
            Status = employee.Status,
            Phone = employee.Phone,
            EmployeePosition = employee.EmployeePosition
        };
        _managerRepository.Employee.Create(newEmployee);
        await _managerRepository.SaveAsync();
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    
}