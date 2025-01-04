using FashionShop_API.Services.Caching;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IServiceManager _serviceManager;
        private readonly IServiceCacheRedis _serviceCacheRedis;
        const string cartKey = "myCart";
        public CartsController(ILogger<CategoriesController> logger, IServiceManager serviceManager, IServiceCacheRedis serviceCacheRedis) {
            _logger = logger;
            _serviceManager = serviceManager;
            _serviceCacheRedis = serviceCacheRedis;
        }
        [HttpGet]
        public async Task<IActionResult> getAllCartAsync() {

            return Ok();
        }
    }
}
