using System.Security.Policy;
using FashionShop.Models;
using FashionShop.Models.views;
using FashionShop.Repositories.ManagerRepository;

namespace FashionShop.Services.Employees;

public class EmployeeService : IEmployeeService
{
    private readonly IManagerRepository _managerRepository;
    private readonly IWebHostEnvironment _hostEnvironment;
    public EmployeeService(IManagerRepository managerRepository, IWebHostEnvironment hostingEnvironment)
    {
        _managerRepository = managerRepository;
        _hostEnvironment = hostingEnvironment;
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
        try
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
                EmployeeName = employee.EmployeeName,
                Gender = employee.Gender,
                Avatar = employee.Avatar,
                Status = employee.Status,
                Phone = employee.Phone,
                EmployeePosition = employee.EmployeePosition
            };
            _managerRepository.Employee.CreateAsync(newEmployee);
            await _managerRepository.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<string> HandleSaveFileAsync(IFormFile file, string directory, string[] allowedExtensions)
    {
        try
        {
            var wwwPath = _hostEnvironment.WebRootPath;
            var path = Path.Combine(wwwPath, directory);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var extension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException($"Only {string.Join(",", allowedExtensions)} extensions are allowed");
            }
            //create file name
            var fileName = $"{Guid.NewGuid()}{extension}";
            var fullPath = Path.Combine(path, fileName);
            using var fileStream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return fileName;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteFile(string fileName, string directory)
    {
        var fullPath  = Path.Combine(_hostEnvironment.WebRootPath, directory, fileName);
        if (!Path.Exists(fullPath))
        {
            throw new FileNotFoundException($"file {fileName} is not found");
        }
        File.Delete(fullPath);
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