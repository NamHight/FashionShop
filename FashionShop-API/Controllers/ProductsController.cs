using AutoMapper;
using FashionShop.Services.Products;
using FashionShop_API.Dto.QueryParam;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Filters;
using FashionShop_API.Models;
using FashionShop_API.Services.Caching;
using FashionShop_API.Services.Products;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Mvc;
using FashionShop_API.Services.Views;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace FashionShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IServiceManager _serviceManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductsController(ILogger<ProductsController> logger, IServiceManager serviceManager, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _serviceManager = serviceManager;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery]bool trackChanges = false)
        {
            try
            {
                var products = await _serviceManager.Product.GetAllAsync(trackChanges);
                if (products == null || !products.Any())
                {
                
                    _logger.LogInformation("No products found.");
                    return NotFound("No products available.");
                }

                _logger.LogInformation("Fetched all products successfully.");

     

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllProductsAsync)}: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }
        [HttpGet("/Products/{slug}")]
        public async Task<IActionResult> GetProductsByCategoryId(string slug)
        {
            var products = await _serviceManager.Product.FindProductsByCategoryIdAsync(slug, false);
            if (products == null || !products.Any())
            {
                return NotFound(new { message = "There is no corresponding product!" });
            }
            return Ok(products);
        }
		[HttpGet("{categorySlug}/{productSlug}")]
		public async Task<IActionResult> GetProductDetails(
		[FromRoute] string categorySlug,  // Lấy từ Path Parameters
		[FromRoute] string productSlug   // Lấy từ Path Parameters
	)
		{
			var param = new ParamCategoryProductDto
			{
				CategorySlug = categorySlug,
				ProductSlug = productSlug
			};

			var product = await _serviceManager.Product.GetProductDetailsAsync(param);

			if (product == null)
			{
				return NotFound();
			}

            return Ok(product);
		}

		[HttpGet("SearchProductName")]
		[ServiceFilter(typeof(ValidationFilter))]
		public async Task<IActionResult> SearchProductName([FromQuery] RequestSearchProductDto requestSearchProductDto)
		{
			var result = await _serviceManager.Product.SearchProductsByNameAsync(requestSearchProductDto);
			return Ok(result);
		}
        [HttpGet("{productId}/stats")]
        public async Task<IActionResult> GetProductStats(long productId)
        {
            try
            {
                if (productId <= 0)
                {
                    return BadRequest("Invalid productId.");
                }
                // Lấy số lượt yêu thích của sản phẩm
                var favoriteCount = await _serviceManager.Product.GetFavoritesCountAsync(productId);

                // Lấy số lượt xem của sản phẩm
                var viewsCount = await _serviceManager.Product.GetViewsCountAsync(productId);

                // Lấy điểm đánh giá trung bình của sản phẩm
                var averageReview = await _serviceManager.Product.GetAverageReviewAsync(productId);

                // Tạo response data
                var stats = new
                {
                    FavoritesCount = favoriteCount,
                    ViewsCount = viewsCount,
                    AverageReview = averageReview
                };

                return Ok(stats);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetProductStats)}: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }
        [HttpPost("{productId}/increment-view")]
        public async Task<IActionResult> IncrementProductView(
            [FromRoute] long productId)
        {
            try
            {
                if (productId <= 0)
                {
                    return BadRequest("Invalid productId.");
                }

                // Kiểm tra xem người dùng có đăng nhập không
                if (User.Identity.IsAuthenticated)
                {
                    var customerId = long.Parse(User.FindFirst("CustomerId")?.Value);  // Lấy customerId từ claim (nếu sử dụng JWT)
                    await _serviceManager.Views.AddViewAsync(productId, customerId: customerId, sessionId: null);  // Thêm lượt xem cho sản phẩm đã đăng nhập
                }
                else
                {
                    var sessionId = HttpContext.Session.Id;  // Nếu chưa đăng nhập, dùng sessionId
                    await _serviceManager.Views.AddViewAsync(productId, customerId: null, sessionId: sessionId);  // Thêm lượt xem cho sản phẩm chưa đăng nhập
                }

                return Ok(new { message = "View incremented successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error incrementing view for product {productId}: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetProductSearchByNameAndFilter([FromQuery] RequestProductDto requestProductDto)
        {
            if(requestProductDto is null)
            {
                return BadRequest(new { message = "Argument is not null" });
            }
            if(requestProductDto.minPrice > requestProductDto.maxPrice)
            {
                return BadRequest(new { message = "max price " });
            }
            var pageResult = await _serviceManager.Product.GetProductSearchAndFilterAsync(requestProductDto, false);
            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pageResult.pageInfo));
            return Ok(pageResult.products);
        }
        
    }
}
