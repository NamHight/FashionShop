
using FashionShop.Models;

namespace FashionShop.Repositories.Reviews
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync(bool trackChanges);
        Task<List<Review>> GetPageLinkAsync(int page, int pageSize, bool trackChanges);
        Task<Review?> GetByIdAsync(long id, bool trackChanges);
        void UpdateReview(Review review);
        void DeleteReview(Review review);
    }
}
