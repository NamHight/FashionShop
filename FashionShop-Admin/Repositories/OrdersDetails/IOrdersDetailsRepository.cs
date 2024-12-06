using FashionShop.Models;
using FashionShop.Models.views.OrderDetails;

namespace FashionShop.Repositories.OrdersDetails;

public interface IOrdersDetailsRepository
{
    Task RemoveDetails(long id, bool trackChanges);
}