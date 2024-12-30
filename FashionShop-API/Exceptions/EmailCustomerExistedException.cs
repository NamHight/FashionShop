using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions;

public class EmailCustomerExistedException : ExistedException
{
    public EmailCustomerExistedException(string message) : base($"Email existed: {message}")
    {
    }
}