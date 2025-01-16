using FashionShop_API.Context;
using FashionShop_API.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Repositories.Orders
{
    public class RepositoryOrders : RepositoryBase<Order>, IRepositoryOrders
    {
        public RepositoryOrders(MyDbContext context) : base(context) { }
        public async Task<IEnumerable<Order>?> GetListOrdersByIdAndStatus(long? id, string status, bool trackChanges)
        {
            var listorderdetail = await FindByCondition(item => item.CustomerId.Equals(id) && item.Status.Equals(status), trackChanges)
                .Include(item => item.Customer).Include(item => item.Ordersdetails).ThenInclude(item => item.Product)
                .ThenInclude(item => item.Category).ToListAsync();
            return listorderdetail.AsEnumerable();
        }
        //public async Task<Ordersdetail?> GetOrdersByIdAndStatus(long id, string status, bool trackChanges)
        //{
        //    var order = await FindByCondition(item => item.Order.CustomerId.Equals(id) && item.Order.Status == status, trackChanges).FirstOrDefaultAsync();
        //    return order;
        //}
        //public async Task RemoveOrdersByIdAndStatusPending(Order order)
        //{
        //    Delete(order);
        //}

        public async Task<Order> GetOrderByCustomerIdAndProductIdAsync(long customerId, long productId)
        {
            // Truy vấn đơn hàng của khách hàng với trạng thái 'completed' và kiểm tra sản phẩm
            return await _context.Orders
                .Where(order => order.CustomerId == customerId
                                && order.Status == "completed"
                                && order.Ordersdetails.Any(item => item.ProductId == productId))
                .FirstOrDefaultAsync();
        }
    }
}
