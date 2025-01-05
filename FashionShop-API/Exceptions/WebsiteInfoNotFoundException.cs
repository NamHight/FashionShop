using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions;

public class WebsiteInfoNotFoundException : NotFoundException
{
    public WebsiteInfoNotFoundException(string message) : base(message)
    {
    }
}