using FashionShop.Services.Categories;
using FashionShop.Services.Products;
using FashionShop.Services.Contacts;

namespace FashionShop.Services.ManagerService;

public interface IManagerService
{
    ICategoryService Category { get; }
    IContactService Contact { get; }
    IProductService Product { get; }
}