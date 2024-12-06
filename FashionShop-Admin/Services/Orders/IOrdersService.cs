using FashionShop.Models;
using FashionShop.Models.views.OrderDetails;

namespace FashionShop.Services.Orders;

public interface IOrdersService
{

    Task<List<Order>> GetAllAsync(bool trackChanges);

    Task<Order?> UpdateStatusPayMent(long id,string payment,bool trackChanges);

    Task<Order?> UpdateStatus(long id, string status, bool trackChanges);
    Task<OrdersViewModel?> GetByIdOrdersViewModel(long id, bool trackChanges);
    Task RemoveIdOrders(long id, bool trackChanges);
}