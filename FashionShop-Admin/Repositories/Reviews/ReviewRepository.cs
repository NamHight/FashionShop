using FashionShop.Context;
using FashionShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Repositories.Reviews
{
    public class ReviewRepository : GenericRepo<Review>, IReviewRepository
    {
        public ReviewRepository(MyDbContext context):base(context) { }
        public async Task<List<Review>> GetAllAsync(bool trackChanges)
        {
            var reviews = await FindAll(trackChanges).ToListAsync();
            return reviews;
        }
        public async Task<List<Review>> GetPageLinkAsync(int page, int pageSize, bool trackChanges)
        {
            var reviews = await PageLinkAsync(page, pageSize, trackChanges);
            return reviews;
        }
        public async Task<Review?> GetByIdAsync(long id,bool trackChanges)
        {
            var review = await FindById(item => item.ReviewId == id,trackChanges).FirstOrDefaultAsync();
            return review;
        }
        public void DeleteReview(Review review)
        {
            Delete(review);
        }
        public void UpdateReview(Review review)
        {
            Update(review);
        }
    }
}
