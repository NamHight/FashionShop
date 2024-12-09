using FashionShop.Models;
using FashionShop.Models.views.ReviewViewModels;
using FashionShop.Models.views;
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
        public async Task<ReviewViewModel> GetPageLinkAsync(string nameSearch, int page, int pageSize, bool trackChanges)
        {

            var reviewPages = await _managerRepository.Review.GetPageLinkAsync(page, pageSize, nameSearch, trackChanges);
            var reviews = await _managerRepository.Review.GetCountAsync(nameSearch, trackChanges);
            var result = new ReviewViewModel
            {
                Reviews = reviewPages,
                NameSearch = nameSearch,
                PagingInfo = new PagingInfo
                {
                    TotalItems = reviews,
                    ItemsPerPage = pageSize,
                    CurrentPage = page,
                }
            };
            return result;
        }
        public async Task<Review?> GetByIdAsync(long id, bool trackChanges)
        {
            var review = await _managerRepository.Review.GetByIdAsync(id, trackChanges);
            return review;
        }
        public async Task<bool> UpdateReview(long reviewId, string newState, bool trackChanges)
        {
            var result = await _managerRepository.Review.GetByIdAsync(reviewId, trackChanges);
            if (result != null)
            {
                result.Status = newState;
                _managerRepository.Review.UpdateReview(result);
                await _managerRepository.SaveAsync();
                return true;
            }
            return false;
        }
        public async Task DeleteReview(long id, bool trackChanges)
        {
            var result = await GetByIdAsync(id, trackChanges);
            if (result != null)
            {
                _managerRepository.Review.DeleteReview(result);
                await _managerRepository.SaveAsync();
            }
        }

    }
}
