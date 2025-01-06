using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions
{
    public class PromotionNotFoundException : NotFoundException
    {
        public PromotionNotFoundException(long id) : base($"Promotion with id : {id} not found!")
        {
            
        }
    }
}
