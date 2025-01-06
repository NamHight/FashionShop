using FashionShop_API.Dto;
using FashionShop_API.Dto.RequestDto;

namespace FashionShop_API.Services.Authenticates;

public interface IServiceAuthenticate
{
    Task<RequestAuthenticateRegisterDto?> RegisterAsync(RequestAuthenticateRegisterDto registerDto);
    Task<bool> ValidateTokenAsync(string token);
    Task<ResponseCustomerDto> LoginAsync(RequestAuthenticateLoginDto loginDto, bool trackChanges);
    Task<ResponseTokenDto> CreateTokenAsync(ResponseCustomerDto? customer,bool populateExp,bool remember,bool trackChanges);
    Task<ResponseTokenDto> RefreshToken(RequestTokenDto requestTokenDto);
    void SetTokenCookie(ResponseTokenDto tokenDto, HttpContext httpContext,bool remember);
    Task RemoveTokenCookie(long id, HttpContext httpContext, bool trackChanges);
    Task<ResponseTokenDto> CreateRefreshTokenAsync(ResponseCustomerDto customer,string accessToken, bool populateExp, bool trackChanges);
}