﻿using AutoMapper;
using FashionShop_API.Dto;
using FashionShop_API.Exceptions;
using FashionShop_API.Repositories;
using FashionShop_API.Services;
namespace FashionShop_API.Services.Customers;

public class ServiceCustomer : IServiceCustomer
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public ServiceCustomer( IMapper mapper, IRepositoryManager repositoryManager)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }

    public async Task<ResponseCustomerDto> GetCustomerByIdAsync(long? id, bool trackChanges)
    {
        
        var customer = await _repositoryManager.Customer.GetCustomerByIdAsync(id.Value, trackChanges);
        if (customer is null)
        {
            throw new CustomerNotFoundException("");
        }
        var customerDto = _mapper.Map<ResponseCustomerDto>(customer);
        return customerDto;
    }

    public async Task<ResponseCustomerDto?> GetCustomerByEmailAsync(string email, bool trackChanges)
    {
        var customer = await _repositoryManager.Customer.GetCustomerByEmailAsync(email, trackChanges);
        if (customer is null)
        {
            throw new CustomerNotFoundException(email);
        }
        var customerDto = _mapper.Map<ResponseCustomerDto>(customer);
        return customerDto;
    }
}