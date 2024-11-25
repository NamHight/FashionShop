using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IServiceManager _serviceManager;

        public CategoriesController(ILogger<CategoriesController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _serviceManager.Category.GetAllCategoryAsync(trackChanges: false);
            _logger.Log(LogLevel.Information,"Controller Category: " + nameof(GetAllAsync) + " Success");
            return Ok(categories);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryByIdAsync(long id)
        {
            var category = await _serviceManager.Category.GetCategoryByIdAsync(id, trackChanges: false);
            _logger.Log(LogLevel.Information,"Controller Category: " + nameof(GetCategoryByIdAsync) + " Success");
            return Ok(category);
        }
    }
}
