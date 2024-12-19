namespace FashionShop_API.Dto;

public record ReponseCategoryDto(long CategoryId , string? CategoryName, string? Slug, string? Description, string? Status, DateTime CreatedAt);


