using FashionShop.Models;

namespace FashionShop.Services.Reviews
{
    public interface IReviewService
    {
        Task<List<Review>> GetAllAsync(bool trackChanges);
        Task<List<Review>> GetPageLinkAsync(int page, int pageSize, bool trackChanges);
        Task<Review?> GetByIdAsync(long id, bool trackChanges);
        Task UpdateReview(Review review);
        Task DeleteReview(Review review);
        
    }
}
