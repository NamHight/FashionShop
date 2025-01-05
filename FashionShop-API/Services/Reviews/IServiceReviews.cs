using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;

namespace FashionShop_API.Services.Reviews
{
	public interface IServiceReviews
	{
		Task<IEnumerable<ResponseReviewDto>> FindReviewsByProductIdAsync(long productId, bool trackChanges);
		Task AddReviewAsync(RequestReviewDto review, bool trackChanges);
		Task<Review> UpdateReviewAsync(long id, RequestReviewDto review, bool trackChanges);
		Task DeleteReviewAsync(long id);
	}
}
