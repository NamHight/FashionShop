using AutoMapper;
using FashionShop_API.Models;
using FashionShop_API.Repositories;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Dto.RequestDto; // Giả sử bạn có ReviewResponseDto

namespace FashionShop_API.Services.Reviews
{
	public class ServiceReviews : IServiceReviews
	{
		private readonly IRepositoryManager _managerRepository;
		private readonly IMapper _mapper;

		public ServiceReviews(IRepositoryManager managerRepository, IMapper mapper)
		{
			_managerRepository = managerRepository;
			_mapper = mapper;
		}

		// Phương thức để lấy tất cả các đánh giá cho một sản phẩm
		public async Task<IEnumerable<ResponseReviewDto>> FindReviewsByProductIdAsync(long productId, bool trackChanges)
		{
			try
			{
				// Lấy các đánh giá từ repository
				var reviews = await _managerRepository.Review.GetListReviewByProductId(productId, trackChanges);

				if (reviews == null || !reviews.Any())
				{
					// Nếu không có đánh giá nào, trả về danh sách rỗng
					return Enumerable.Empty<ResponseReviewDto>();
				}

				// Sử dụng AutoMapper để chuyển đổi từ entity Review sang ReviewResponseDto
				var reviewDtos = _mapper.Map<IEnumerable<ResponseReviewDto>>(reviews);

				return reviewDtos;
			}
			catch (Exception ex)
			{
				// Ghi log và throw exception nếu có lỗi xảy ra
				// _logger.LogError(ex, "Error retrieving reviews.");
				throw new Exception("An error occurred while retrieving reviews.", ex);
			}
		}

		public async Task AddReviewAsync(RequestReviewDto review, bool trackChanges)
		{
			// Chuyển đổi RequestReviewDto thành entity Review
			var reviewEntity = new Review
			{
				ProductId = review.ProductId,
				CustomerId = review.CustomerId,
				ReviewText = review.ReviewText,
				Rating = review.Rating,
				ReviewDate = DateTime.UtcNow
			};

			// Thêm vào cơ sở dữ liệu
			await _managerRepository.Review.AddAsync(reviewEntity,trackChanges);
		}
		public async Task<Review> UpdateReviewAsync(long id, RequestReviewDto review, bool trackChanges)
		{
			var reviewEntity = new Review
			{
				ReviewId = id,
				ProductId = review.ProductId,
				CustomerId = review.CustomerId,
				ReviewText = review.ReviewText,
				Rating = review.Rating,
				ReviewDate = DateTime.UtcNow
			};

			// Cập nhật review với trackChanges
			await _managerRepository.Review.UpdateAsync(reviewEntity, trackChanges);

			return reviewEntity;
		}
		public async Task DeleteReviewAsync(long id)
		{
			// Gọi phương thức DeleteAsync trong repository để thực hiện xóa review
			await _managerRepository.Review.DeleteAsync(id);
		}
	}
}
