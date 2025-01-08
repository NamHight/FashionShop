using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;

namespace FashionShop_API.Repositories.Reviews
{
	public interface IRepositoryReviews
	{
		Task<IEnumerable<Review>?> GetListReviewByProductId(long productId, bool trackChanges);
		Task AddAsync(Review entity, bool trackChanges);
		Task UpdateAsync(Review entity, bool trackChanges);
		Task DeleteAsync(long id);
	}
}
