namespace FashionShop_API.Dto.ResponseDto
{
     public record ResponseProductDto(long ProductId, string ProductName, string? Banner, string? Description, decimal? Price, long? Quantity, long CategoryId);
}
