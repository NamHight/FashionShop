using AutoMapper;
using FashionShop.Services.Products;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;
using FashionShop_API.Services.Caching;
using FashionShop_API.Services.Products;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop_API.Controllers
{
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
        [Route("api/[controller]")]
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

    }
}
