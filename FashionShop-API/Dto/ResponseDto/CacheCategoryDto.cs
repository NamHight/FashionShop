namespace FashionShop_API.Dto;

public record CacheCategoryDto(IEnumerable<ReponseCategoryDto> CategoriesDto, PageInfo PageInfo);
