namespace FashionShop_API.Exceptions.Base;

public abstract class ExistedException : Exception
{
    protected ExistedException(string message) : base(message)
    {
        
    }
}