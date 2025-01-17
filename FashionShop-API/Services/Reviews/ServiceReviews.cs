using AutoMapper;
using FashionShop_API.Models;
using FashionShop_API.Repositories;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Exceptions.Base;
using FashionShop_API.Exceptions;
using FashionShop_API.Dto; // Giả sử bạn có ReviewResponseDto

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
		public async Task<(IEnumerable<ResponseReviewDto> data, PageInfo page)> FindReviewsByProductIdAsync(int page, int limit, long productId, string typeOrderBy)
		{

			var reviews = await _managerRepository.Review.GetListReviewByProductId(page, limit, productId, typeOrderBy);
			if (reviews.Count == 0)
			{
				throw new ReviewNotFoundException("Product has no reviews yet");
			}
			var reviewDtos = _mapper.Map<IEnumerable<ResponseReviewDto>>(reviews);

			return (data: reviewDtos, page: reviews.PageInfo);
		}

		public async Task AddReviewAsync(RequestReviewDto review, bool trackChanges)
		{
			// Chuyển đổi RequestReviewDto thành entity Review
			var reviewEntity = new Review
			{
				ProductId = review.ProductId,
				CustomerId = review.CustomerId,
				ReviewText = review.ReviewText,
				Image=review.Image,
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

        public async Task<double?> TotalReviewRatingAsync(long productId)
        {
            if(productId <= 0)
			{
				throw new TotalReviewBadRequestException(productId);
			}
			try
			{
               var totalReviewRating =  await _managerRepository.Review.TotalReviewRatingAsync(productId);
				if(totalReviewRating == null)
				{
                    throw new PageNotFoundException("Rating");
                }
				return totalReviewRating;
            }
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
				throw new PageNotFoundException("Rating");
			}
        }
    }
}
