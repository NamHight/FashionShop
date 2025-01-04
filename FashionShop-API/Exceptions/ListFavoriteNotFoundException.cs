using FashionShop_API.Exceptions.Base;
namespace FashionShop_API.Exceptions
{
    public class ListFavoriteNotFoundException: NotFoundException
    {
        public ListFavoriteNotFoundException(string message) : base($"List Favorites not found {message}")
        {
        }
    }
}
