using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions
{
    public class ProductNotFoundException :NotFoundException
    {
        public ProductNotFoundException(string message) : base($"Product not found {message}")
        {
        }
    }
}
