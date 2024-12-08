using FashionShop.Models.views.ReviewViewModels;
using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Mvc;
using FashionShop.Models.views;
namespace FashionShop.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IManagerService _serviceManager;
        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(IManagerService serviceManager, ILogger<ReviewsController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string nameSearch, int page = 1, int pageSize = 10)
        {
            var result = await _serviceManager.Review.GetPageLinkAsync(nameSearch, page, pageSize, trackChanges: false);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateState(long reviewId, string newState)
        {
            var result = await _serviceManager.Review.UpdateReview(reviewId, newState, trackChanges: true);
            if (result)
            {
                return Json(new { success = true, message = "Trạng thái đã được cập nhật!" });
            }
            return Json(new { success = false, message = "Không tìm thấy đánh giá cần đổi trạng thái!" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long ReviewId)
        {
            await _serviceManager.Review.DeleteReview(ReviewId, trackChanges: false);
            return RedirectToAction("Index");
        }

    }
}
