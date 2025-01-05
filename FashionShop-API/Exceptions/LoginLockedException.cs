using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions;

public class LoginLockedException : ForbiddenException
{
    public LoginLockedException(string message) : base(message)
    {
    }
}