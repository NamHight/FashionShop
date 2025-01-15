using FashionShop_API.Models;
namespace FashionShop_API.Repositories.Orders
{
    public interface IRepositoryOrders
    {
        Task<IEnumerable<Order>?> GetListOrdersByIdAndStatus(long? id, string status, bool trackChanges);
        Task<Order> GetOrderByCustomerIdAndProductIdAsync(long customerId, long productId);
        Task AddOrder(Order order);
        Task<Order> FindOrderById(long? id, bool trackChanges);
    }
}
