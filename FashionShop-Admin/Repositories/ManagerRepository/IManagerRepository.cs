﻿using FashionShop.Repositories.Categories;
using FashionShop.Repositories.Contacts;
using FashionShop.Repositories.Employees;
using FashionShop.Repositories.Roles;
using FashionShop.Repositories.Stores;

namespace FashionShop.Repositories.ManagerRepository;

public interface IManagerRepository
{
    ICategoryRepository Category { get; }

    IContactRepository Contact { get; } 
    
    IEmployeeRepository Employee { get; }
    
    IRoleRepository Role { get; }
    
    IStoreRepository Store { get; }
    
    Task SaveAsync();
}