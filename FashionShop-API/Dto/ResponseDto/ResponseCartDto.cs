namespace FashionShop_API.Dto.ResponseDto
{
    public record ResponseCartDto (long ProductId,  string? Banner, string ? ProductName,  decimal? Price, int? Quantity, decimal? Amount);

  
}
