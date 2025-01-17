using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions
{
    public class OrderCancelIsNull: NotFoundException
    {
        public OrderCancelIsNull(string message):base(message) { }
    }
}
