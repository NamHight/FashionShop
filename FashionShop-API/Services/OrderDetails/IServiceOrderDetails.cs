using FashionShop_API.Models;
namespace FashionShop_API.Services.OrderDetails
{
    public interface IServiceOrderDetails
    {
        Task AddOrderDetail(Ordersdetail orderDetail);
    }
}
