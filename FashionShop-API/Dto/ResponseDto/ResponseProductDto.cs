namespace FashionShop_API.Dto.ResponseDto
{
     public record ResponseProductDto(long ProductId, string Slug ,string ProductName, string? Banner, string? Description, decimal? Price, long? Quantity, long CategoryId);
}
