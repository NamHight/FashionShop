﻿using FashionShop.Models;
using FashionShop.Models.views;

namespace FashionShop.Services.Employees;

public interface IEmployeeService
{
    Task<Employee?> LoginAsync(string email, string password,bool trackChanges);
    Task<IEnumerable<Employee>> GetAllAsync(bool trackChanges);

    Task CreateAsync(CreateEmployeeViewModel employee);
}