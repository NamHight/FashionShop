using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions
{
    public class ReviewNotFoundException : NotFoundException
    {
        public ReviewNotFoundException(string message) : base(message)
        {
        }
    }
}
