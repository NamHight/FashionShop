using FashionShop.Models;
using FashionShop.Models.views.OrderDetails;

namespace FashionShop.Repositories.Orders;

public interface IOrdersRepository
{
    Task<List<Order>> GetAllAsync(bool trackChanges);
    Task<Order?> GetByIdOrders(long id, bool trackChanges);
    Task<Order?> GetByIdAsync(long id, bool trackChanges);
    Task<Order?> GetByIdOrdersViewModel(long id, bool trackChanges);
    Task Remove(Order order);

 
}