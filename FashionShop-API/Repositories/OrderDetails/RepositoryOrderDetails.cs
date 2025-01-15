using FashionShop_API.Context;
using FashionShop_API.Models;
using FashionShop_API.Repositories.OrderDetails;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Repositories.OrderDetails
{
    public class RepositoryOrderDetails : RepositoryBase<Ordersdetail>, IRepositoryOrderDetails
    {
        public RepositoryOrderDetails(MyDbContext context) : base(context) { }
       
        public async Task AddOrderDetail(Ordersdetail orderDetail)
        {
            await Create(orderDetail);
        }
    }
}