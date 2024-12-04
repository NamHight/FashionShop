using FashionShop.Models;
using FashionShop.Models.views;

namespace FashionShop.Services.Employees;

public interface IEmployeeService
{
    Task<Employee?> LoginAsync(string email, string password,bool trackChanges);
    Task<IEnumerable<Employee>> GetAllAsync(bool trackChanges);

    Task CreateAsync(CreateEmployeeViewModel employee);
    Task<string> HandleSaveFileAsync(IFormFile file, string directory, string[] allowedExtensions);
    
    Task<bool> CheckUniqueEmail(string email,bool trackChanges);

    Task<EditEmployeeViewModel?> GetByIdEditAsync(long id, bool trackChanges);
    
    Task<bool> UpdateAsync(long id, EditEmployeeViewModel employee,bool trackChanges);

    Task<bool> ChangeAvatar(string avatar, long id,bool trackChanges);
    
    Task<bool> ChangePassword(ChangePasswordModel model, long id,bool trackChanges);

    Task<bool> CheckPassword(long id, string password);
}