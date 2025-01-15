using AutoMapper;
using FashionShop.Services.Products;
using FashionShop_API.Dto.QueryParam;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;
using FashionShop_API.Services.Caching;
using FashionShop_API.Services.Products;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FashionShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IServiceManager _serviceManager;
        public ProductsController(ILogger<ProductsController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
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
        [HttpGet("/Products/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategoryId(long categoryId)
        {
            var products = await _serviceManager.Product.FindProductsByCategoryIdAsync(categoryId, false);
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

	}
}
