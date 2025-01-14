using FashionShop_API.Dto.QueryParam;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Services.ServiceLogger;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FashionShop_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewsController : ControllerBase
	{
		private readonly IServiceManager _serviceManager;
		private readonly ILoggerManager _loggerManager;

		public ReviewsController(IServiceManager serviceManager, ILoggerManager loggerManager)
		{
			_serviceManager = serviceManager;
			_loggerManager = loggerManager;
		}

		[HttpGet]
		public async Task<IActionResult> GetReviewsByProductId([FromQuery] ParamReviewDto paramReviewDto)
		{
			var reviews = await _serviceManager.Review.FindReviewsByProductIdAsync(paramReviewDto.Page, paramReviewDto.Limit, paramReviewDto.ProductId, paramReviewDto.TypeOrderBy);
			Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(reviews.page));
			return Ok(reviews.data);
		}

		[HttpPost]
		public async Task<IActionResult> AddReview([FromBody] RequestReviewDto request)
		{
			if (request == null)
			{
				// Trả về BadRequest nếu request là null
				_loggerManager.LogError("Invalid review data.");
				return BadRequest(new { message = "Dữ liệu đánh giá không hợp lệ!" });
			}

			// Kiểm tra nếu các thông tin cần thiết không hợp lệ (ví dụ: thiếu nội dung, điểm số, v.v.)
			if (string.IsNullOrWhiteSpace(request.ReviewText) || request.Rating < 1 || request.Rating > 5)
			{
				_loggerManager.LogError("Invalid review content or rating.");
				return BadRequest(new { message = "Đánh giá không hợp lệ. Vui lòng kiểm tra lại nội dung và điểm số." });
			}

			try
			{
				// Gọi dịch vụ để thêm review vào cơ sở dữ liệu
				await _serviceManager.Review.AddReviewAsync(request,true);

				// Trả về kết quả thành công
				return Ok(new { message = "Đánh giá đã được thêm thành công!" });
			}
			catch (Exception ex)
			{
				// Log lỗi nếu có
				_loggerManager.LogError($"Something went wrong inside the AddReview action: {ex.Message}");
				return StatusCode(500, new { message = "Đã có lỗi xảy ra. Vui lòng thử lại sau!" });
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateReview(long id, [FromBody] RequestReviewDto request, [FromQuery] bool trackChanges = true)
		{
			if (request == null)
			{
				_loggerManager.LogError("Review data is null.");
				return BadRequest(new { message = "Dữ liệu đánh giá không hợp lệ!" });
			}

			if (string.IsNullOrWhiteSpace(request.ReviewText) || request.Rating < 1 || request.Rating > 5)
			{
				_loggerManager.LogError("Invalid review data.");
				return BadRequest(new { message = "Đánh giá không hợp lệ. Vui lòng kiểm tra lại nội dung và điểm số." });
			}

			try
			{
				// Cập nhật review vào cơ sở dữ liệu với trackChanges = true (hoặc false tùy vào yêu cầu)
				var updatedReview = await _serviceManager.Review.UpdateReviewAsync(id, request, true);

				if (updatedReview == null)
				{
					return NotFound(new { message = "Không tìm thấy đánh giá để cập nhật." });
				}

				return Ok(new { message = "Đánh giá đã được cập nhật thành công!" });
			}
			catch (Exception ex)
			{
				_loggerManager.LogError($"Something went wrong in UpdateReview: {ex.Message}");
				return StatusCode(500, new { message = "Đã có lỗi xảy ra khi cập nhật đánh giá." });
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteReview(long id)
		{
			try
			{
				// Gọi service để xóa review
				await _serviceManager.Review.DeleteReviewAsync(id);

				return Ok(new { message = "Đánh giá đã được xóa thành công!" });
			}
			catch (KeyNotFoundException)
			{
				return NotFound(new { message = "Không tìm thấy đánh giá để xóa." });
			}
			catch (Exception ex)
			{
				_loggerManager.LogError($"Something went wrong in DeleteReview: {ex.Message}");
				return StatusCode(500, new { message = "Đã có lỗi xảy ra khi xóa đánh giá." });
			}
		}
		[HttpGet("TotalReviewRating/{productId}")]
		public async Task<IActionResult> GetTotalReviewAsync(long productId)
		{
			var totalReview = await _serviceManager.Review.TotalReviewRatingAsync(productId);
			return Ok(totalReview);
		}
	}
}
