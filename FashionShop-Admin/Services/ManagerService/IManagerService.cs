using FashionShop.Services.Categories;
using FashionShop.Services.Products;
using FashionShop.Services.Contacts;
using FashionShop.Services.Reviews;
using FashionShop.Services.Customers;
using FashionShop.Services.Employees;
using FashionShop.Services.Roles;
using FashionShop.Services.Stores;
using FashionShop.Services.Orders;
using FashionShop.Services.OrdersDetails;
using FashionShop.Services.WebsiteInfos;

namespace FashionShop.Services.ManagerService;

public interface IManagerService
{
    ICategoryService Category { get; }
    IContactService Contact { get; }
    IEmployeeService Employee { get; }
    IStoreService Store { get; }
    IRoleService Role { get; }
    IProductService Product { get; }
    IReviewService Review { get; }
    ICustomerService Customer { get; }
    IOrdersService Orders { get; }
    IOrdersDetailsService OrdersDetails { get; }
    IWebsiteService Website { get; }
}