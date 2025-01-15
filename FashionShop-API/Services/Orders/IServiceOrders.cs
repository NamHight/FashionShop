using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;
namespace FashionShop_API.Services.Orders
{
    public interface IServiceOrders
    {
        Task<IEnumerable<ResponseOrdersDto>?> GetListOrdersByIdAndStatus(long? id, string status, bool trackChanges);
        //Task RemoveOrdersByIdAndStatus(long id, string status, bool trackChanges);
        Task<bool> HasPurchasedProductAsync(long customerId, long productId);
        Task <Order>AddOrder(RequestOrderDto order);
    }
}
