namespace FashionShop_API.Exceptions.Base;

public abstract class UnauthorizedException : Exception
{
    protected UnauthorizedException(string message) : base(message)
    {
        
    }
}