namespace FashionShop_API.Dto.ResponseDto
{
		public record ResponseReviewDto(long ReviewId, sbyte? Rating, string? ReviewText, DateTime? ReviewDate, long? CustomerId, string? Image, long? ProductId, ResponseCustomerDto Customer);
	
}
