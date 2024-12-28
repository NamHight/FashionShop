using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Filters;
using FashionShop_API.Services.ServiceLogger;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILoggerManager _loggerManager;
        public TokenController(IServiceManager serviceManager, ILoggerManager loggerManager)
        {
            _serviceManager = serviceManager;
            _loggerManager = loggerManager;
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> RefreshToken([FromBody]RequestTokenDto requestTokenDto)
        {
            var result = await _serviceManager.Authenticate.RefreshToken(requestTokenDto);
            _loggerManager.LogInfo("Controller Token: " + nameof(RefreshToken) + " Success");
            return Ok(result);
        }
        
    }
}
