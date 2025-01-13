using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions
{
    public class TotalReviewBadRequestException : BadRequestException
    {
        public TotalReviewBadRequestException(long id) : base($"The request is invalid due to incorrect data or format with {id}.")
        {
        }
    }
}
