using FashionShop.Context;
using FashionShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Repositories.Reviews
{
    public class ReviewRepository : GenericRepo<Review>, IReviewRepository
    {
        public ReviewRepository(MyDbContext context):base(context) { }
        public async Task<int> GetCountAsync(string nameSearch, bool trackChanges)
        {
            if (!string.IsNullOrEmpty(nameSearch))
            {
                return await FindById(item => item.ReviewText.Contains(nameSearch), trackChanges).CountAsync();
            }
            return await FindAll(trackChanges).CountAsync();
        }
        public async Task<List<Review>> GetPageLinkAsync(int page, int pageSize, string nameSearch, bool trackChanges)
        {
            if (!string.IsNullOrEmpty(nameSearch))
            {
                return await PageLinkAsync(page, pageSize, trackChanges).Include(r => r.Product).Include(r => r.Customer).Where(r => r.ReviewText.Contains(nameSearch)).ToListAsync();
            }
            return await PageLinkAsync(page, pageSize, trackChanges).Include(r => r.Product).Include(r => r.Customer).ToListAsync();
        }
        public async Task<Review?> GetByIdAsync(long id, bool trackChanges)
        {
            var review = await FindById(item => item.ReviewId == id, trackChanges).FirstOrDefaultAsync();
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
