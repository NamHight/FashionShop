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
       public async Task<Order> FindOrderById(long? id, bool trackChanges)
        {
            var order = await FindByCondition(item => item.OrderId.Equals(id), trackChanges).FirstOrDefaultAsync();
            return order;
        }
    }
}
