using FashionShop_API.Dto;

namespace FashionShop_API.Services.Customers;

public interface IServiceCustomer
{
    Task<ResponseCustomerDto> GetCustomerByIdAsync(long? id, bool trackChanges);
}