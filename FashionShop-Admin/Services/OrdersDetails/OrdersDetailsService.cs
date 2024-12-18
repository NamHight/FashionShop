using FashionShop.Models;
using FashionShop.Models.views.OrderDetails;
using FashionShop.Repositories.ManagerRepository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FashionShop.Services.OrdersDetails;

public class OrdersDetailsService : IOrdersDetailsService
{
    private readonly IManagerRepository _managerRepository;

    public OrdersDetailsService(IManagerRepository managerRepository) {
        _managerRepository = managerRepository;
    }

    public async Task RemoveByIdOrdersDetails(long id, bool trackChanges)
    {
        try
        {
            await _managerRepository.OrdersDetails.RemoveDetails(id, trackChanges);
            await _managerRepository.SaveAsync();
        }catch (Exception e)
        {
            Console.WriteLine($"Lỗi xoá orderdetails {e}");
            throw;
        }
        
    }
}