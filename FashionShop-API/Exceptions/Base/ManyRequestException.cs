namespace FashionShop_API.Exceptions.Base;

public abstract class ManyRequestException : Exception
{
    protected ManyRequestException(string message) : base(message)
    {
        
    }
}