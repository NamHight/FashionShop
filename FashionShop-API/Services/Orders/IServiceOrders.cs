using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;
namespace FashionShop_API.Services.Orders
{
    public interface IServiceOrders
    {
        Task<IEnumerable<ResponseOrdersDto>?> GetListOrdersByIdAndStatus(long? id, string status, bool trackChanges);
        Task OrderCancel(RequestOrderCancelDto order, bool trackChanges);
    }
}
