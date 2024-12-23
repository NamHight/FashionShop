
using System.Text.Json;
using FashionShop_API.Dto;
using FashionShop_API.Dto.QueryParam;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Exceptions;
using FashionShop_API.Filters;
using FashionShop_API.Services.Caching;
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
        private readonly IServiceCacheRedis _serviceCacheRedis;
        public CategoriesController(ILogger<CategoriesController> logger, IServiceManager serviceManager, IServiceCacheRedis serviceCacheRedis)
        {
            _logger = logger;
            _serviceManager = serviceManager;
            _serviceCacheRedis = serviceCacheRedis;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery]ParamCategoryDto paramCategoryDto)
        {
            if (paramCategoryDto.Page <= 0)
            {
                throw new PageNotFoundException(paramCategoryDto.Page.ToString());
            }
            string keyCaching = $"Categories-{paramCategoryDto.SearchTerm}-{paramCategoryDto.Page}-{paramCategoryDto.Limit}-{paramCategoryDto.SortBy}-{paramCategoryDto.SortOrder}";
            var categoriesCache = await _serviceCacheRedis.GetData<CacheCategoryDto>(keyCaching);
            if (categoriesCache is not null)
            {
                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(categoriesCache.PageInfo));
                return Ok(categoriesCache.CategoriesDto);
            }
            var categories = await _serviceManager.Category.GetAllPaginatedAndSearchAndSortAsync(paramCategoryDto);
            var cacheCategory = new CacheCategoryDto(categories.data, categories.meta);
            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(categories.meta));
            await _serviceCacheRedis.SetData(keyCaching,cacheCategory);
            _logger.Log(LogLevel.Information,"Controller Category: " + nameof(GetAllAsync) + " Success");
            return Ok(categories.data);
        }
        
        [ResponseCache(Duration = 30)]
        [HttpGet("{id}", Name = "GetCategoryById")]
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
