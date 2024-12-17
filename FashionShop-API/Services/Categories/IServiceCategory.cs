using FashionShop_API.Dto;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Models;

namespace FashionShop_API.Services.Categories;

public interface IServiceCategory
{
    Task<IEnumerable<CategoryDto>> GetAllCategoryAsync(bool trackChanges);
    Task<CategoryDto?> GetCategoryByIdAsync(long id, bool trackChanges);
    Task<ResponsePage<CategoryDto>> GetAllPaginatedAsync(int page, int limit);
    Task<RequestCategoryDto> CreateAsync(RequestCategoryDto categoryDto);
    
}