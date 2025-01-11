using FashionShop_API.Dto;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Filters;
using FashionShop_API.Services.Caching;
using FashionShop_API.Services.Emails;
using FashionShop_API.Services.ServiceManager;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("Register")]
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
            await _serviceEmail.SendEmailConfirmAsync(customerToken,"ConfirmEmail","ConfirmTemplate");
            _logger.Log(LogLevel.Information,"Controller Authenticate: " + nameof(RegisterAsync) + " Success");
            return StatusCode(201);
        }
        [HttpPost("Login")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> LoginAsync([FromBody] RequestAuthenticateLoginDto requestAuthenticateLoginDto,bool remember)
        {
            string keyRedis = $"Authenticate-{requestAuthenticateLoginDto.Email}-{requestAuthenticateLoginDto.Password}-{remember}";
            if (IsCheckConnectionRedis())
            {
                var resultCaching = await _cacheRedis.GetData<ResponseCustomerDto>(keyRedis);
                _logger.Log(LogLevel.Information,"Controller Authenticate: " + nameof(LoginAsync) + " Success");
                if (resultCaching is not null)
                {
                    var tokenCaching = await _serviceManager.Authenticate.CreateTokenAsync(resultCaching,false,remember,false);
                    _serviceManager.Authenticate.SetTokenCookie(tokenCaching, HttpContext,remember);
                    return Ok(tokenCaching);
                }
            }
            var result = await _serviceManager.Authenticate.LoginAsync(requestAuthenticateLoginDto, false);
            if (IsCheckConnectionRedis())
            {
                await _cacheRedis.SetData(keyRedis, result);
            }
            _logger.Log(LogLevel.Information,"Controller Authenticate: " + nameof(LoginAsync) + " Success");
            var tokenDto = await _serviceManager.Authenticate.CreateTokenAsync(result,true,remember,false);
            _serviceManager.Authenticate.SetTokenCookie(tokenDto,HttpContext,remember);
            return Ok(tokenDto);
        }
        [Authorize( Policy = "MultiAuth")]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout(long id)
        {
            await _serviceManager.Authenticate.RemoveTokenCookie(id,HttpContext,false);
            _logger.Log(LogLevel.Information,"Controller Authenticate: " + nameof(Logout) + " Success");
            return Ok("Logout success");
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            if (!await _serviceManager.Authenticate.ValidateTokenAsync(token))
            {
                return Redirect("http://localhost:3000/email-confirmation-error");
            }   
            return Redirect("http://localhost:3000/email-confirmation");
        }
        [HttpPost("LoginGoogle")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> LoginGoogleAsync([FromBody] GoogleSignVM googleSignVM)
        {
            var result = await _serviceManager.Google.GoogleSignIn(googleSignVM);
            if (result is null)
            {
                return BadRequest(new { message = "Registration failed. Please try again later." });
            }
            var token = await _serviceManager.Authenticate.CreateRefreshTokenAsync(result,googleSignVM.Token,true,true);
            _serviceManager.Authenticate.SetTokenCookie(token, HttpContext, false);
            return Ok(token);
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody]string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                BadRequest("Email is null");
            }
            var token = await _serviceEmail.HandleSendEmail(email, false);
            if (token is null)
            {
                BadRequest("Token is null");
            }
            else
            {
                await _serviceEmail.SendEmailConfirmAsync(token, "ForgotPasswordConfirm","ResetTemplate");
            }
            _logger.Log(LogLevel.Information, "Controller Authenticate: " + nameof(ForgotPassword) + " Success");
            return Ok("Send email success");
        }
        [HttpGet("ForgotPasswordConfirm")]
        public async Task<IActionResult> ForgotPasswordConfirm([FromQuery]string token)
        {
            var result = await _serviceManager.Authenticate.ValidateTokenPasswordAsync(token);
            if (result)
            {
                return Redirect($"http://localhost:3000/reset-password?token={token}");
            }
            return Redirect("http://localhost:3000/error-404");
        }
        [HttpPost("ResetPassword")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> ResetPassword([FromBody] RequestResetPasswordDto? requestResetPasswordDto)
        {
            if (requestResetPasswordDto is null || string.IsNullOrWhiteSpace(requestResetPasswordDto.token))
            {
                BadRequest("Invalid Request");
            }
            var result = await _serviceManager.Authenticate.ForgotPasswordAsync(requestResetPasswordDto);
            if (!result)
            {
                return BadRequest("Invalid or expired token");
            }
            _logger.Log(LogLevel.Information, "Controller Authenticate: " + nameof(ResetPassword) + " Success");
            return Ok("Reset password success");
        }
        
    }
}
