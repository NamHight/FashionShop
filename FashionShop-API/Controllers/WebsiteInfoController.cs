using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WebsiteInfoController: ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly ILogger<WebsiteInfoController> _logger;
    public WebsiteInfoController(IServiceManager serviceManager, ILogger<WebsiteInfoController> logger)
    {
        _serviceManager = serviceManager;
        _logger = logger;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetWebsiteInfo()
    {
        var result = await _serviceManager.WebsiteInfo.GetWebsiteInfoAsync(false);
        _logger.Log(LogLevel.Information,"Controller WebsiteInfo: " + nameof(GetWebsiteInfo) + " Success");
        return Ok(result);
    }
}