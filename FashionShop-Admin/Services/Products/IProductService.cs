﻿using FashionShop.Models;

namespace FashionShop.Services.Products
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync(bool trackChanges);
    }
}
