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

        public async Task<IActionResult> Index(int Page = 1, int PageSize = 10)
        {
            var reviewPages = await _serviceManager.Review.GetPageLinkAsync(Page, PageSize, trackChanges : false);
            var reviews = await _serviceManager.Review.GetAllAsync(trackChanges: false);
            var result = new ReviewViewModel
            {
                Reviews = reviewPages,
                Products = await _serviceManager.Product.GetAllAsync(trackChanges: false),
                Customers = await _serviceManager.Customer.GetAllAsync(trackChanges: false),
                PagingInfo = new PagingInfo
                {
                    TotalItems = reviews.Count(),
                    ItemsPerPage = PageSize,
                    CurrentPage = Page,
                }
            };
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateState(long reviewId,string newState)
        {
            var result = await _serviceManager.Review.GetByIdAsync(reviewId, trackChanges: true);
            if (result != null)
            {
                result.Status = newState;
                await _serviceManager.Review.UpdateReview(result);
                return Json(new { success = true, message = "Trạng thái đã được cập nhật!" });
            }
            return Json(new { success = false, message = "Không tìm thấy đánh giá cần đổi trạng thái!" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long ReviewId)
        {
            var result = await _serviceManager.Review.GetByIdAsync(ReviewId, trackChanges: false);
            if (result != null)
            {
                await _serviceManager.Review.DeleteReview(result);
            }
            return RedirectToAction("Index");
        }
    }
}
