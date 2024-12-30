namespace FashionShop_API.Dto;

public record ResponseCategoryDto(long CategoryId , string? CategoryName, string? Slug, string? Description, string? Status, DateTime CreatedAt);


