using FashionShop_API.Dto.QueryParam;
using FashionShop_API.Services.Articles;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FashionShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ArticlesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/<ArticlesController>
        [HttpGet]
        public async Task<ActionResult> GetPagingAndSearchAsync([FromQuery] ParamArticleDto paramArticleDto)
        {
            var result = await _serviceManager.Article.GetPagingAndSearchAsync(paramArticleDto);
            return Ok(result);
        }

        
    }
}
