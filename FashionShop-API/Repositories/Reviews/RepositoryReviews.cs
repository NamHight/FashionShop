using FashionShop_API.Context;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;
using FashionShop_API.Repositories.Favorites;
using FashionShop_API.Repositories.Shared;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Repositories.Reviews
{
	public class RepositoryReviews : RepositoryBase<Review>, IRepositoryReviews
	{
		public RepositoryReviews(MyDbContext context) : base(context)
		{
		}

		public async Task<PagedListAsync<Review>> GetListReviewByProductId(int page, int limit, long productId, string typeOrderBy)
		{
			if (typeOrderBy == "asc")
			{
				var reviews = FindByCondition(item => item.ProductId == productId, false).OrderBy(item => item.ReviewDate).Include(item => item.Customer);
				return await PagedListAsync<Review>.ToPagedListAsync(reviews, page, limit);
			}
            return await PagedListAsync<Review>.ToPagedListAsync(_context.Reviews.AsNoTracking().Where(item => item.ProductId == productId).OrderByDescending(item => item.ReviewDate).Include(item => item.Customer).AsQueryable(), page, limit);
        }
		public async Task AddAsync(Review entity, bool trackChanges)
		{
			if (!trackChanges)
			{
				_context.Entry(entity).State = EntityState.Added;
			}
			else
			{
				await _context.Set<Review>().AddAsync(entity);
			}
			await _context.SaveChangesAsync();
		}
		public async Task UpdateAsync(Review entity, bool trackChanges)
		{
			var existingReview = await _context.Set<Review>().FindAsync(entity.ReviewId);

			if (existingReview == null)
			{
				throw new KeyNotFoundException("Review not found.");
			}

			if (trackChanges)
			{
				_context.Set<Review>().Update(existingReview);
			}
			else
			{
				_context.Entry(existingReview).State = EntityState.Modified;
			}

			// Cập nhật các trường của review hiện tại
			existingReview.ReviewText = entity.ReviewText;
			existingReview.Rating = entity.Rating;
			existingReview.CreatedAt = entity.CreatedAt;

			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(long id)
		{
			var review = await _context.Set<Review>().FindAsync(id);

			if (review == null)
			{
				throw new KeyNotFoundException("Review not found.");
			}

			_context.Set<Review>().Remove(review);
			await _context.SaveChangesAsync(); // Lưu các thay đổi vào DB
		}

        public async Task<double?> TotalReviewRatingAsync(long productId)
        {
			return await _context.Reviews.AsNoTracking().Where(item => item.ProductId == productId).AverageAsync(item => item.Rating);
        }
        public async Task<List<Review>> GetListReviewByProductIdAsync(long productId)
        {
            // Truy vấn các review cho sản phẩm với productId
            return await _context.Reviews
                                 .Where(r => r.ProductId == productId)
                                 .ToListAsync();
        }
    }
}
