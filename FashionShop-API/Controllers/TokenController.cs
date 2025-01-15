using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Filters;
using FashionShop_API.Services.Caching;
using FashionShop_API.Services.ServiceLogger;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop_API.Controllers
{
    [Authorize(Policy ="MultiAuth")]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IServiceCacheRedis _serviceCacheRedis;
        public TokenController(IServiceManager serviceManager, ILoggerManager loggerManager, IServiceCacheRedis serviceCacheRedis)
        {
            _serviceManager = serviceManager;
            _loggerManager = loggerManager;
            _serviceCacheRedis = serviceCacheRedis;
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            HttpContext.Request.Cookies.TryGetValue("access_token", out var tokenCookie);
            HttpContext.Request.Cookies.TryGetValue("refresh_token", out var refreshToken);
            var tokenDto = new RequestTokenDto(tokenCookie!,refreshToken!);
            var result = await _serviceManager.Authenticate.RefreshToken(tokenDto);
            _serviceManager.Authenticate.SetTokenCookie(result, HttpContext, false);
            _loggerManager.LogInfo("Controller Token: " + nameof(RefreshToken) + " Success");
            return Ok(result);
        }
        [Authorize(Policy = "MultiAuth")]
        [HttpGet("ProtectData")]
        public async Task<IActionResult> ProtectData()
        {
            HttpContext.Request.Cookies.TryGetValue("access_token", out var tokenAccess);
            _loggerManager.LogInfo("Controller Token: " + nameof(ProtectData) + " Success");
            if (tokenAccess is null)
            {
                return Unauthorized(new {Message = "Access token is missing or invalid."});
            }
            return Ok(new {token = tokenAccess});
        }
    }
}
