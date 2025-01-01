using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions;

public class CustomerNotFoundException : NotFoundException
{
    public CustomerNotFoundException(string message) : base($"Email not found {message}")
    {
    }
}