using FashionShop_API.Services.ServiceLogger;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop_API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly ILoggerManager _loggerManager;
    public CustomerController(IServiceManager serviceManager, ILoggerManager loggerManager)
    {
        _serviceManager = serviceManager;
        _loggerManager = loggerManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomerByIdAsync(long? id)
    {
        if (id is null)
        {
            return BadRequest("Id is null");
        }
        _loggerManager.LogInfo("Controller Customer: " + nameof(GetCustomerByIdAsync) + " Success");
        var result = await _serviceManager.Customer.GetCustomerByIdAsync(id,false);
        return Ok(result);
    }
}