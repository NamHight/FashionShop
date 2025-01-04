using FashionShop_API.Dto;
using FashionShop_API.Dto.QueryParam;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Models;

namespace FashionShop_API.Services.Categories;

public interface IServiceCategory
{
    Task<IEnumerable<ResponseCategoryChildrenDto>> GetAllCategoryAsync(bool trackChanges);
    Task<ResponseCategoryDto?> GetCategoryByIdAsync(long id, bool trackChanges);
    Task<(IEnumerable<ResponseCategoryDto> data,PageInfo meta)> GetAllPaginatedAsync(int page, int limit);
    Task<(IEnumerable<ResponseCategoryDto> data ,PageInfo meta)> GetAllPaginatedAndSearchAndSortAsync(ParamCategoryDto paramCategoryDto);
    Task<RequestCategoryDto> CreateAsync(RequestCategoryDto categoryDto);
}