using FashionShop.Services.Categories;
using FashionShop.Services.Products;

namespace FashionShop.Services.ManagerService;

public interface IManagerService
{
    ICategoryService Category { get; }

    IProductService Product { get; }
}