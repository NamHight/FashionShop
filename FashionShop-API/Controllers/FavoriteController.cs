using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Services.ServiceLogger;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop_API.Controllers
{
    [Authorize(Policy = "MultiAuth")]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILoggerManager _loggerManager;

        public FavoriteController(IServiceManager serviceManager, ILoggerManager loggerManager)
        {
            _serviceManager = serviceManager;
            _loggerManager = loggerManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetListFavoriteByIdAsync(long? id)
        {
            if (id is null)
            {
                return BadRequest("Id is null");
            }
            _loggerManager.LogInfo("Controller Favorite: " + nameof(GetListFavoriteByIdAsync) + " Success");
            var result = await _serviceManager.Favorite.GetListFavoritesByIdAsync(id, false);
            return Ok(result);
        }

        
        [HttpPost]
		public async Task<IActionResult> AddFavorite([FromBody] RequestFarvoriteDto request)
		{
			//_loggerManager.LogInfo("Controller Customer: " + nameof(AddFavorite) + " Success");
			if (request == null)
			{
				// Trả về BadRequest nếu request là null
				_loggerManager.LogError("Invalid review data.");
				return BadRequest(new { message = "Dữ liệu đánh giá không hợp lệ!" });
			}

			try
			{
				await _serviceManager.Favorite.AddFavoriteAsync(request, true);
				return Ok(new { message = "Yêu thích sản phẩm thành công" });
			}
			catch (Exception ex)
			{
				// Log lỗi nếu có
				_loggerManager.LogError($"Something went wrong inside the AddFarvorite action: {ex.Message}");
				return StatusCode(500, new { message = "Đã có lỗi xảy ra. Vui lòng thử lại sau!" });
			}
		}
        [HttpDelete]
        public async Task<IActionResult> DeleteFavorite(long userId, long productId)
        {
            try
            {
                bool result = await _serviceManager.Favorite.DeleteFavoriteAsync(userId, productId);

                if (result)
                {
                    return Ok(new { message = "Yêu thích đã được xóa thành công!" });
                }
                else
                {
                    return NotFound(new { message = "Không tìm thấy yêu thích để xóa." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã có lỗi xảy ra khi xóa yêu thích.", error = ex.Message });
            }
        }
    }
}
