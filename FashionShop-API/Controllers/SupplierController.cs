using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly IServiceManager _serviceManager;
        public SupplierController(ILogger<SupplierController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSuppilersAsync([FromQuery] bool trackChanges = false)
        {
            try
            {
                var suppilers = await _serviceManager.Suppiler.GetAllAsync(trackChanges);
                if (suppilers == null || !suppilers.Any())
                {

                    _logger.LogInformation("No suppiles found.");
                    return NotFound("No suppilers available.");
                }

                _logger.LogInformation("Fetched all products successfully.");
                return Ok(suppilers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllSuppilersAsync)}: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
