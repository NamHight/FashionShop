using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions;

public class SlugExistedException : ExistedException
{
    public SlugExistedException(string message) : base("Slug existed: " + message)
    {
    }
}