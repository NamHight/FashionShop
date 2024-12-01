using FashionShop.Services.Categories;
using FashionShop.Services.Contacts;
using FashionShop.Services.Employees;
using FashionShop.Services.Roles;
using FashionShop.Services.Stores;

namespace FashionShop.Services.ManagerService;

public interface IManagerService
{
    ICategoryService Category { get; }
    IContactService Contact { get; }
    IEmployeeService Employee { get; }
    IStoreService Store { get; }
    IRoleService Role { get; }
}