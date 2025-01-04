using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions;

public class CategoryListNotFoundException : NotFoundException
{
    public CategoryListNotFoundException(string message) : base(message)
    {
    }
}