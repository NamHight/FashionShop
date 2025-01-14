using FashionShop_API.Models;
namespace FashionShop_API.Repositories.Orders
{
    public interface IRepositoryOrders
    {
        Task<IEnumerable<Order>?> GetListOrdersByIdAndStatus(long? id, string status, bool trackChanges);
        //Task<Order?> GetOrdersByIdAndStatus(long id, string status, bool trackChanges);
        //Task RemoveOrdersByIdAndStatusPending(Order order);
        Task<Order> GetOrderByCustomerIdAndProductIdAsync(long customerId, long productId);
        Task<Order> FindOrderById(long? id, bool trackChanges);
    }
}
