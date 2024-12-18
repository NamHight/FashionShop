using FashionShop.Models;
using FashionShop.Models.views.OrderDetails;
using FashionShop.Repositories.ManagerRepository;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Reflection;

namespace FashionShop.Services.Orders;

public class OrdersService : IOrdersService
{
    private readonly IManagerRepository _managerRepository;
    public OrdersService(IManagerRepository managerRepository) => _managerRepository = managerRepository;

    public async Task<List<Order>> GetAllAsync(bool trackChanges)
    {
        var orders = await _managerRepository.Orders.GetAllAsync(trackChanges);
        return orders;
    }

    public async Task<OrdersViewModel?> GetByIdOrdersViewModel(long id, bool trackChanges)
    {
        try
        {
            var order = await _managerRepository.Orders.GetByIdOrdersViewModel(id, trackChanges);
            var ordersViewModel = new OrdersViewModel
            {
                OrderId = order.OrderId,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                PaymentMethod = order.PaymentMethod,
                StoreName = order.Store.StoreName,
                CustomerName = order.Customer.CustomerName,
                EmployeeName = order.Employee.EmployeeName,
                OrdersStatus = order.Status,
                AddressStore = order.Store.Address,
                PhoneStore = order.Store.Phone,
                CustomerAddress = order.Customer.Address,
                PhoneCustomer = order.Customer.Phone,
                Details = order.Ordersdetails.ToList()
            };
            return ordersViewModel;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi không tìm thấy {ex}");
            throw;
        }
    }
    public async Task<Order?> UpdateStatusPayMent(long id, string payment, bool trackChanges)
    {
        var order = await _managerRepository.Orders.GetByIdAsync(id, trackChanges);
        if (order == null)
        {
            return null;
        }
        order.PaymentMethod = payment;
        await _managerRepository.SaveAsync();
        return order;
    }
    public async Task<Order?> UpdateStatus(long id, string status, bool trackChanges)
    {
        var order = await _managerRepository.Orders.GetByIdAsync(id, trackChanges);
        if (order == null)
        {
            return null;
        }
        order.Status = status;
        await _managerRepository.SaveAsync();
        return order;
    }

    public async Task RemoveIdOrders(long id, bool trackChanges)
    {
        try
        {
            var order = await _managerRepository.Orders.GetByIdAsync(id, trackChanges);
            if (order == null)
            {
                throw new Exception();
            }
            await _managerRepository.OrdersDetails.RemoveDetails(id, trackChanges);
            await _managerRepository.Orders.Remove(order);
            await _managerRepository.SaveAsync();
        }catch (Exception e)
        {
            Console.WriteLine($"Lỗi xoá order {e}");
            throw;
        }
        
    }
    public async Task<int> GetOrderCountAsync()
    {
        return await _managerRepository.Orders.CountOrderAsync();
    }
}