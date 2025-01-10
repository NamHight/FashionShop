using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions
{
    public class ArticleNotFoundException : NotFoundException
    {
        public ArticleNotFoundException(string message) : base($"Article with nameSearch {message} not found!")
        {
        }
    }
}
