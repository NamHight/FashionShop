using FashionShop.Models;
using FashionShop.Repositories.ManagerRepository;

namespace FashionShop.Services.Reviews
{
    public class ReviewService : IReviewService
    {
        private readonly IManagerRepository _managerRepository;
        public ReviewService(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }
        public async Task<List<Review>> GetAllAsync(bool trackChanges)
        {
            var reviews = await _managerRepository.Review.GetAllAsync(trackChanges);
            return reviews;
        }
        public async Task<List<Review>> GetPageLinkAsync(int page, int pageSize, bool trackChanges)
        {
            var reviews = await _managerRepository.Review.GetPageLinkAsync(page, pageSize, trackChanges);
            return reviews;
        }
        public async Task<Review?> GetByIdAsync(long id, bool trackChanges)
        {
            var review = await _managerRepository.Review.GetByIdAsync(id, trackChanges);
            return review;
        }
        public async Task UpdateReview(Review review)
        {
            _managerRepository.Review.UpdateReview(review);
            await _managerRepository.SaveAsync();
        }
        public async Task DeleteReview(Review review)
        {
            _managerRepository.Review.DeleteReview(review);
            await _managerRepository.SaveAsync();
        }
    }
}
