namespace FashionShop_API.Dto.ResponseDto
{
    public record ResponsePromotionDto(long PromotionId, string PromotionName, string Image, string Description, DateTime StartDate, DateTime EndDate, decimal DiscountRate);

}
