using FashionShop_API.Dto;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Filters;
using FashionShop_API.Services.Caching;
using FashionShop_API.Services.Emails;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ILogger<AuthenticateController> _logger;
        private readonly IServiceManager _serviceManager;
        private readonly IServiceEmail _serviceEmail;
        private readonly IServiceCacheRedis _cacheRedis;
        public AuthenticateController(ILogger<AuthenticateController> logger, IServiceManager serviceManager, IServiceEmail serviceEmail, IServiceCacheRedis cacheRedis)
        {
            _logger = logger;
            _serviceManager = serviceManager;
            _serviceEmail = serviceEmail;
            _cacheRedis = cacheRedis;
        }

        private bool IsCheckConnectionRedis() => _cacheRedis.CheckConnection();
        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> RegisterAsync([FromBody] RequestAuthenticateRegisterDto requestAuthenticateRegisterDto)
        {
            var user = await _serviceManager.Authenticate.RegisterAsync(requestAuthenticateRegisterDto);
            if (user is null)
            {
                return BadRequest(new { message = "Registration failed. Please try again later." });
            }
            var customerToken = await _serviceEmail.HandleSendEmail(user.Email,false);
            if (customerToken is null)
            {
                return BadRequest(new {message = "Registration failed. Please try again later."});
            }
            await _serviceEmail.SendEmailConfirmAsync(customerToken);
            _logger.Log(LogLevel.Information,"Controller Authenticate: " + nameof(RegisterAsync) + " Success");
            return StatusCode(201);
        }
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> LoginAsync([FromBody] RequestAuthenticateLoginDto requestAuthenticateLoginDto)
        {
            string keyRedis = $"Authenticate-{requestAuthenticateLoginDto.Email}";
            if (IsCheckConnectionRedis())
            {
                var resultCaching = await _cacheRedis.GetData<ResponseCustomerDto>(keyRedis);
                _logger.Log(LogLevel.Information,"Controller Authenticate: " + nameof(LoginAsync) + " Success");
                if (resultCaching is not null)
                {
                    var tokenCaching = await _serviceManager.Authenticate.CreateTokenAsync(resultCaching,false,false);
                    return Ok(tokenCaching);
                }
            }
            var result = await _serviceManager.Authenticate.LoginAsync(requestAuthenticateLoginDto, false);
            if (IsCheckConnectionRedis())
            {
                await _cacheRedis.SetData(keyRedis, result);
            }
            _logger.Log(LogLevel.Information,"Controller Authenticate: " + nameof(LoginAsync) + " Success");
            var tokenDto = await _serviceManager.Authenticate.CreateTokenAsync(result,true,false);
            return Ok(tokenDto);
        }
        
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            if (!await _serviceManager.Authenticate.ValidateTokenAsync(token))
            {
                return BadRequest("Invalid token");
            }
            return Ok("Email confirmed");
        }
    }
}
