using FashionShop_API.Dto;
using FashionShop_API.Dto.RequestDto;

namespace FashionShop_API.Services.ServiceAuthenticate;

public interface IServiceAuthenticate
{
    Task<RequestAuthenticateRegisterDto?> RegisterAsync(RequestAuthenticateRegisterDto registerDto);
    Task<bool> ValidateTokenAsync(string token);
    Task<ResponseCustomerDto> LoginAsync(RequestAuthenticateLoginDto loginDto, bool trackChanges);
    Task<ResponseTokenDto> CreateTokenAsync(ResponseCustomerDto? customer, bool populateExp,bool trackChanges);
    Task<ResponseTokenDto> RefreshToken(RequestTokenDto requestTokenDto);
}