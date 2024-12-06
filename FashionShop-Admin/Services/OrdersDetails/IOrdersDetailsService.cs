using FashionShop.Models;
using FashionShop.Models.views.OrderDetails;

namespace FashionShop.Services.OrdersDetails;

public interface IOrdersDetailsService
{

    Task RemoveByIdOrdersDetails(long id, bool trackChanges);
}