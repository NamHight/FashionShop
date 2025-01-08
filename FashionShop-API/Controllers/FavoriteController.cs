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
		public async Task<IActionResult> AddFarvorite([FromBody] RequestFarvoriteDto request)
		{
			_loggerManager.LogInfo("Controller Customer: " + nameof(AddFarvorite) + " Success");
			if (request == null)
			{
				// Trả về BadRequest nếu request là null
				_loggerManager.LogError("Invalid review data.");
				return BadRequest(new { message = "Dữ liệu đánh giá không hợp lệ!" });
			}

			try
			{
				await _serviceManager.Favorite.AddFarvoriteAsync(request, true);
				return Ok(new { message = "Yêu thích sản phẩm thành công" });
			}
			catch (Exception ex)
			{
				// Log lỗi nếu có
				_loggerManager.LogError($"Something went wrong inside the AddFarvorite action: {ex.Message}");
				return StatusCode(500, new { message = "Đã có lỗi xảy ra. Vui lòng thử lại sau!" });
			}
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFarvorite(long id)
		{
			_loggerManager.LogInfo("Controller Customer: " + nameof(DeleteFarvorite) + " Success");

			try
			{
				await _serviceManager.Favorite.DeleteFarvoriteAsync(id);

				return Ok(new { message = "Yêu thích được xóa thành công!" });
			}
			catch (KeyNotFoundException)
			{
				return NotFound(new { message = "Không tìm thấy yêu thích để xóa." });
			}
			catch (Exception ex)
			{
				_loggerManager.LogError($"Something went wrong in DeleteFarvorite: {ex.Message}");
				return StatusCode(500, new { message = "Đã có lỗi xảy ra khi xóa yêu thích." });
			}
		}
	}
}
