
using System.Text.Json;
using FashionShop_API.Dto.QueryParam;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Exceptions;
using FashionShop_API.Filters;
using FashionShop_API.Services.ServiceManager;
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
        public async Task<IActionResult> GetAllAsync([FromQuery]ParamCategoryDto paramCategoryDto)
        {
            if (paramCategoryDto.Page <= 0)
            {
                throw new PageNotFoundException(paramCategoryDto.Page.ToString());
            }
            var categories = await _serviceManager.Category.GetAllPaginatedAsync(paramCategoryDto.Page,10);
            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(categories.meta));
            _logger.Log(LogLevel.Information,"Controller Category: " + nameof(GetAllAsync) + " Success");
            return Ok(categories.data);
        }

        [HttpGet("{name}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryByIdAsync(long id)
        {
            var category = await _serviceManager.Category.GetCategoryByIdAsync(id, trackChanges: false);
            _logger.Log(LogLevel.Information,"Controller Category: " + nameof(GetCategoryByIdAsync) + " Success");
            return Ok(category);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> CreateCategory([FromBody]RequestCategoryDto? requestCategoryDto)
        {
            var category = await _serviceManager.Category.CreateAsync(requestCategoryDto);
            return CreatedAtRoute("GetCategoryById", new { id = category.CategoryId }, category);
        }
        
    }
}
