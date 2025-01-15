using AutoMapper;
using FashionShop_API.Repositories;
using FashionShop_API.Repositories.Orders;
using FashionShop_API.Models;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Exceptions;
using FashionShop_API.Dto.RequestDto;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FashionShop_API.Services.Orders
{
    public class ServiceOrders : IServiceOrders
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mappers;
        public ServiceOrders(IRepositoryManager repositoryManager, IMapper mappers)
        {
            _repositoryManager = repositoryManager;
            _mappers = mappers;
        }
        public async Task<IEnumerable<ResponseOrdersDto>?> GetListOrdersByIdAndStatus(long? id, string status, bool trackChanges)
        {
            try
            {
                var listorder = await _repositoryManager.Orders.GetListOrdersByIdAndStatus(id, status, trackChanges);
                if (listorder is null)
                {
                    throw new ListOrdersNotFoundException("");
                }
                var listorderdto = _mappers.Map<IEnumerable<ResponseOrdersDto>>(listorder);
                return listorderdto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("-- Lỗi ", ex);
                throw;
            }
        }
        public async Task<bool> HasPurchasedProductAsync(long customerId, long productId)
        {
            // Gọi repository để kiểm tra đơn hàng của người dùng
            var order = await _repositoryManager.Orders.GetOrderByCustomerIdAndProductIdAsync(customerId, productId);
            return order != null;
        }

        public async Task<Order> AddOrder(RequestOrderDto order)
        {
            if (order is null) throw new ListOrdersNotFoundException("Order rong");
            var orderDomain = _mappers.Map<Order>(order);
            await _repositoryManager.Orders.AddOrder(orderDomain);
            // sau khi SaveChanges thì orderDomain sẽ được gán ID;
            await _repositoryManager.SaveChanges();
            return orderDomain;
        }
        public async Task<bool> HasPurchasedProductAsync(long customerId, long productId)
        {
            // Gọi repository để kiểm tra đơn hàng của người dùng
            var order = await _repositoryManager.Orders.GetOrderByCustomerIdAndProductIdAsync(customerId, productId);
            return order != null;
        }
        public async Task OrderCancel(RequestOrderCancelDto? order, bool trackChanges)
        {
            try
            {
                if (order is null) throw new OrderCancelIsNull("order not found");
                var getOrder = await _repositoryManager.Orders.FindOrderById(order.OrderId, trackChanges);
                if (getOrder is null) throw new OrderCancelIsNull("order not found");
                getOrder.Status = order.Status;
                getOrder.ReasonCancel = order.ReasonCancel;
                await _repositoryManager.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("-- Lỗi ", ex);
                throw;
            }
        }
    }
}
