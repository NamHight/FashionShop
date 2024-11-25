namespace FashionShop_API.Dto;

public record CategoryDto(long CategoryId , string? CategoryName, string? Slug, string? Description, string? Status, DateTime CreatedAt);


