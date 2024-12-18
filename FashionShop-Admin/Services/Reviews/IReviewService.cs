using FashionShop.Models;
using FashionShop.Models.views.ReviewViewModels;

namespace FashionShop.Services.Reviews
{
    public interface IReviewService
    {
        Task<ReviewViewModel> GetPageLinkAsync(string nameSearch, int page, int pageSize, bool trackChanges);
        Task<Review?> GetByIdAsync(long id, bool trackChanges);
        Task<bool> UpdateReview(long reviewId, string newState, bool trackChanges);
        Task DeleteReview(long id, bool trackChanges);

    }
}
