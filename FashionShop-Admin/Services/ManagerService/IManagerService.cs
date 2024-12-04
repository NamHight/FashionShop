using FashionShop.Services.Categories;
using FashionShop.Services.Products;
using FashionShop.Services.Contacts;
using FashionShop.Services.Reviews;
using FashionShop.Services.Customers;

namespace FashionShop.Services.ManagerService;

public interface IManagerService
{
    ICategoryService Category { get; }
    IContactService Contact { get; }
    IProductService Product { get; }
    IReviewService Review { get; }
    ICustomerService Customer { get; }
}