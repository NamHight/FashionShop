using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions;

public class WrongPasswordException : UnauthorizedException
{
    public WrongPasswordException(string message) : base(message)
    {
    }
}