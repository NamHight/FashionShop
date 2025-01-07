namespace FashionShop_API.Dto.ResponseDto
{
     public record ResponseProductDto(long ProductId, long CategoryId, string ProductName, string? Banner, int? Quantity, decimal? Price);
}
