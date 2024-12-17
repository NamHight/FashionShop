using System.Net;
using FashionShop_API.Dto;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Exceptions;
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
        public async Task<IActionResult> GetAllAsync(int page = 1,int limit = 10)
        {
            if (page <= 0)
            {
                throw new PageNotFoundException(page.ToString());
            }
            var categories = await _serviceManager.Category.GetAllPaginatedAsync(page,limit);
            _logger.Log(LogLevel.Information,"Controller Category: " + nameof(GetAllAsync) + " Success");
            return Ok(categories);
        }

        [HttpGet("{name}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryByIdAsync(long id)
        {
            var category = await _serviceManager.Category.GetCategoryByIdAsync(id, trackChanges: false);
            _logger.Log(LogLevel.Information,"Controller Category: " + nameof(GetCategoryByIdAsync) + " Success");
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody]RequestCategoryDto? requestCategoryDto)
        {
            if (requestCategoryDto is null)
            {
                return BadRequest("Category is null");
            }
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var category = await _serviceManager.Category.CreateAsync(requestCategoryDto);
            return CreatedAtRoute("GetCategoryById", new { id = category.CategoryId }, category);
        }
        
    }
}
