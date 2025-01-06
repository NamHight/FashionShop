using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions;

public class PageNotFoundException : NotFoundException
{
    public PageNotFoundException(string message) : base("Page not found: " + message)
    {
    }
}