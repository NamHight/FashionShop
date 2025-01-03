using FashionShop_API.Services.ServiceLogger;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILoggerManager _loggerManager;

        public FavoriteController(IServiceManager serviceManager, ILoggerManager loggerManager)
        {
            _serviceManager = serviceManager;
            _loggerManager = loggerManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetListFavoriteByIdAsync(long? id)
        {
            if (id is null)
            {
                return BadRequest("Id is null");
            }
            _loggerManager.LogInfo("Controller Customer: " + nameof(GetListFavoriteByIdAsync) + " Success");
            var result = await _serviceManager.Favorite.GetListFavoritesByIdAsync(id, false);
            return Ok(result);
        }
    }
}
