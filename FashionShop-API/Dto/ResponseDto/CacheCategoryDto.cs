namespace FashionShop_API.Dto;

public record CacheCategoryDto(IEnumerable<ResponseCategoryDto> CategoriesDto, PageInfo PageInfo);
