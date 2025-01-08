using FashionShop_API.Context;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;
using FashionShop_API.Repositories.Favorites;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Repositories.Reviews
{
	public class RepositoryReviews : RepositoryBase<Review>, IRepositoryReviews
	{
		public RepositoryReviews(MyDbContext context) : base(context)
		{
		}

		public async Task<IEnumerable<Review>?> GetListReviewByProductId(long productId, bool trackChanges)
		{
			// Truy vấn tất cả các đánh giá liên quan đến sản phẩm với ID 'productId'
			var reviews = await FindByCondition(r => r.ProductId == productId, trackChanges)
				.Include(r => r.Product) // Bao gồm thông tin sản phẩm
				.Include(r => r.Product.Category) // Bao gồm thông tin danh mục của sản phẩm
				.Include(r => r.Product.Store) // Bao gồm thông tin cửa hàng bán sản phẩm
				.ToListAsync();

			// Trả về danh sách các review hoặc danh sách rỗng nếu không có đánh giá nào
			return reviews.Any() ? reviews : Enumerable.Empty<Review>();
		}
		public async Task AddAsync(Review entity, bool trackChanges)
		{
			if (!trackChanges)
			{
				// Nếu không cần theo dõi thay đổi, tắt tracking
				_context.Entry(entity).State = EntityState.Added;
			}
			else
			{
				// Nếu cần theo dõi thay đổi, mặc định Entity Framework sẽ tự động track
				await _context.Set<Review>().AddAsync(entity);
			}

			// Lưu thay đổi vào cơ sở dữ liệu
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
	}
}
