using FashionShop_API.Exceptions.Base;

namespace FashionShop_API.Exceptions;

public class CategoryNotFoundException : NotFoundException
{
    public CategoryNotFoundException(long  id) : base($"Category with id: {id} was not found.")
    {
    }
}