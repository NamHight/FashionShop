using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions;

public class LoginManyTimeException : ManyRequestException
{
    public LoginManyTimeException(string message) : base(message)
    {
    }
}