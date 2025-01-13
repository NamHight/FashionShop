using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;
using FashionShop_API.Repositories.Shared;

namespace FashionShop_API.Repositories.Reviews
{
	public interface IRepositoryReviews
	{
		Task<PagedListAsync<Review>> GetListReviewByProductId(int page, int limit, long productId, string typeOrderBy);
		Task AddAsync(Review entity, bool trackChanges);
		Task UpdateAsync(Review entity, bool trackChanges);
		Task DeleteAsync(long id);
		Task<double?> TotalReviewRatingAsync(long productId);
        Task<List<Review>> GetListReviewByProductIdAsync(long productId);
    }
}
