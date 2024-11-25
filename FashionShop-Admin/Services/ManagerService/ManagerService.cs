﻿using FashionShop.Repositories.ManagerRepo;
using FashionShop.Services.Categories;
using FashionShop.Services.Products;
namespace FashionShop.Services.ManagerService;

public class ManagerService : IManagerService
{
    private readonly Lazy<ICategoryService> _categoryService;
    private readonly Lazy<IProductService> _productService;

    public ManagerService(IManagerRepository managerRepository)
    {
        _categoryService = new Lazy<ICategoryService>(() => new CategoryService(managerRepository));
        _productService = new Lazy<IProductService>(() => new ProductService(managerRepository));
    }
    
    public ICategoryService Category => _categoryService.Value;
    public IProductService Product => _productService.Value;

}