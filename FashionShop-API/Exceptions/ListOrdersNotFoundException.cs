using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions
{
    public class ListOrdersNotFoundException : NotFoundException
    {
        public ListOrdersNotFoundException(string message) : base($"List Orders not found {message}")
        {}
    }
}
