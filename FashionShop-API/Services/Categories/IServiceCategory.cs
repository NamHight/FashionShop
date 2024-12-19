using FashionShop_API.Dto;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Models;

namespace FashionShop_API.Services.Categories;

public interface IServiceCategory
{
    Task<IEnumerable<ReponseCategoryDto>> GetAllCategoryAsync(bool trackChanges);
    Task<ReponseCategoryDto?> GetCategoryByIdAsync(long id, bool trackChanges);
    Task<(IEnumerable<ReponseCategoryDto> data,PageInfo meta)> GetAllPaginatedAsync(int page, int limit);
    Task<RequestCategoryDto> CreateAsync(RequestCategoryDto categoryDto);
    
}