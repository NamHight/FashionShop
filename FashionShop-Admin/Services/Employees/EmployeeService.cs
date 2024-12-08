using System.Security.Policy;
using FashionShop.Models;
using FashionShop.Models.views;
using FashionShop.Models.views.EmployeeViewModel;
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
                Birth = employee.Birth,
                Address = employee.Address,
                Description = employee.Description,
                Status = employee.Status,
                Phone = employee.Phone,
                EmployeePosition = employee.EmployeePosition,
                CreatedAt = DateTime.Now
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

    public async Task<bool> CheckUniqueEmail(string email, bool trackChanges)
    {
        var employee = await _managerRepository.Employee.CheckUniqueEmail(email, trackChanges);
        return employee;
    }

    public async Task<EditEmployeeViewModel?> GetByIdEditAsync(long id, bool trackChanges)
    {
        var employee = await _managerRepository.Employee.GetByIdWithRoleAndStoreAsync(id, trackChanges);
        if (employee == null)
        {
            return null;
        }

        var newEditEmployee = new EditEmployeeViewModel
        {
            Email = employee.Email,
            EmployeePosition = employee.EmployeePosition,
            Avatar = employee.Avatar,
            Status = employee.Status,
            Phone = employee.Phone,
            EmployeeName = employee.EmployeeName,
            Gender = employee.Gender,
            RoleName = employee.Role?.RoleName,
            StoreName = employee.Store?.StoreName,
            Address = employee.Address,
            Description = employee.Description,
            Birth = employee.Birth,
            EmployeeId = employee.EmployeeId,
            StoreId = employee.StoreId,
            RoleId = employee.RoleId
        };

        return newEditEmployee;
    }

    public async Task<bool> UpdateAsync(long id,EditEmployeeViewModel employee,bool trackChanges)
    {
        try
        {
            var currentEmployee = await _managerRepository.Employee.GetById(id, trackChanges);
            if (currentEmployee == null)
            {
                return false;
            };
            if (employee.AvatarFile != null)
            {
                if (employee.Avatar != null && employee.Avatar.Length > 1 * 1024 * 1024)
                {
                    throw new InvalidOperationException("Avatar must be less than 1MB");
                }
                currentEmployee.Avatar = await HandleSaveFileAsync(employee.AvatarFile, "uploaded", new[] { ".jpg", ".png", ".jpeg" });
            }
            currentEmployee.EmployeePosition = employee.EmployeePosition;
            currentEmployee.Status = employee.Status;
            currentEmployee.Phone = employee.Phone;
            currentEmployee.EmployeeName = employee.EmployeeName;
            currentEmployee.Gender = employee.Gender;
            currentEmployee.RoleId = employee.RoleId;
            currentEmployee.StoreId = employee.StoreId;
            currentEmployee.Address = employee.Address;
            currentEmployee.Description = employee.Description;
            currentEmployee.Birth = employee.Birth;
            currentEmployee.UpdatedAt = DateTime.Now;
            await _managerRepository.Employee.UpdateAsync(currentEmployee);
            await _managerRepository.SaveAsync();
            return true;
        }catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> ChangeAvatar(string avatar, long id,bool trackChanges)
    {
        var employee = await _managerRepository.Employee.GetById(id,trackChanges);
        if (employee == null)
        {
            return false;
        }
        employee.Avatar = avatar;
        await _managerRepository.SaveAsync();
        return true;
    }

    public async Task<bool> ChangePassword(ChangePasswordModel model, long id, bool trackChanges)
    {
        var employee = await _managerRepository.Employee.GetById(id, trackChanges);
        if (employee == null)
        {
            return false;
        }
        if (string.IsNullOrWhiteSpace(model.Password) || !VerifyPassword(model.Password, employee.Password))
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(model.NewPassword))
        {
            return false;
        }
        employee.Password = HashPassword(model.NewPassword);
        await _managerRepository.SaveAsync();
        return true;
    }

    public async Task<bool> CheckPassword(long id, string password,bool trackChanges)
    {
        var employee = await _managerRepository.Employee.GetById(id,trackChanges);
        if (employee == null)
        {
            return false;
        }
        return VerifyPassword(password, employee.Password);
    }

    public async Task<EmployeeViewModel> GetPaginateAllAsync(int page, int limit, bool trackChanges)
    {
        var employees = await _managerRepository.Employee.GetPaginateAllAsync(page, limit, trackChanges);
        var count = await _managerRepository.Employee.CountAsync();
        
        var result = new EmployeeViewModel
        {
            Employees =employees,
            PagingInfo = new PagingInfo
            {
                TotalItems = count,
                CurrentPage = page,
                ItemsPerPage = limit,
            }
            
        };
        return result;
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