namespace FashionShop_API.Dto.ResponseDto
{
    public record ProductDto(long ProductId, string ProductName, string? Banner, int? Quantity, decimal? Price);
}
