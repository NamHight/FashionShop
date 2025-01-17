using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions
{
    public sealed class MaxPriceRangeBadRequestException:BadRequestException
    {
        public MaxPriceRangeBadRequestException(): base("Max Price can't be less than min price.")
        {

        }
    }
}
