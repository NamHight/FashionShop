namespace FashionShop_API.Dto.ResponseDto
{
    public record ResponseCartDto (long ProductId, string ProductName, string? Banner, int? Quantity, decimal? Price, decimal? Amount);

  
}
