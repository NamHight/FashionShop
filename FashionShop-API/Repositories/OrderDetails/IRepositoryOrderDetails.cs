using FashionShop_API.Models;
namespace FashionShop_API.Repositories.OrderDetails
{
    public interface IRepositoryOrderDetails
    {
        Task AddOrderDetail(Ordersdetail orderDetail);
    }
}
