using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions;

public class RefreshTokenException : BadRequestException
{
    public RefreshTokenException(string message) : base($"Invalid client request. The tokenDto has some invalid values.")
    {
    }
}