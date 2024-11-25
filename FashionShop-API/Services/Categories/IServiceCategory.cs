using FashionShop_API.Dto;
using FashionShop_API.Models;

namespace FashionShop_API.Services.Categories;

public interface IServiceCategory
{
    Task<IEnumerable<CategoryDto>> GetAllCategoryAsync(bool trackChanges);
    Task<CategoryDto?> GetCategoryByIdAsync(long id, bool trackChanges);
}