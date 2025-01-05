using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using FashionShop_API.Dto;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Exceptions;
using FashionShop_API.Models;
using FashionShop_API.Repositories;
using FashionShop_API.Services.ServiceLogger;
using Microsoft.IdentityModel.Tokens;


namespace FashionShop_API.Services.Authenticates;

public class ServiceAuthenticate : IServiceAuthenticate
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _loggerManager;
    private readonly IConfiguration _configuration;
    public ServiceAuthenticate(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager loggerManager, IConfiguration configuration)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _loggerManager = loggerManager;
        _configuration = configuration;
    }
    public async Task<ResponseCustomerDto> LoginAsync(RequestAuthenticateLoginDto loginDto,bool trackChanges)
    {
        var foundCustomer = await _repositoryManager.Customer.GetCustomerByEmailAsync(loginDto.Email, trackChanges);
        if (foundCustomer is null )
        {
            _loggerManager.LogInfo("Service Authenticate: " + nameof(LoginAsync) + " Fail");
            throw new CustomerNotFoundException(loginDto.Email);
        }
        if (foundCustomer.LockoutEnd <= DateTime.UtcNow)
        {
            foundCustomer.LockoutEnd = null;
            foundCustomer.LockoutEnabled = false;
            foundCustomer.AccessFailedCount = 0;
            _repositoryManager.Customer.UpdateCustomer(foundCustomer);
            await _repositoryManager.SaveChanges();
            var resultAfterLock = _mapper.Map<ResponseCustomerDto>(foundCustomer);
            return resultAfterLock;
        }
        if (foundCustomer.LockoutEnabled is true)
        {
            _loggerManager.LogInfo("Service Authenticate: " + nameof(LoginAsync) + " Fail");
            throw new LoginLockedException("Your account has been locked. Please try again later.");
        }
        if ( foundCustomer.LockoutEnd > DateTime.UtcNow)
        {
            _loggerManager.LogInfo("Service Authenticate: " + nameof(LoginAsync) + " Fail");
            throw new LoginManyTimeException("Too many failed login attempts . Please try again later 5 minutes.");
        }
        if (!VerifyPassword(loginDto.Password, foundCustomer.Password))
        {
            _loggerManager.LogInfo("Service Authenticate: " + nameof(LoginAsync) + " Fail");
            foundCustomer.AccessFailedCount += 1;
            if (foundCustomer.AccessFailedCount == 5)
            {
                foundCustomer.LockoutEnabled = true;
                foundCustomer.LockoutEnd = DateTime.UtcNow.AddMinutes(5);
                _repositoryManager.Customer.UpdateCustomer(foundCustomer);
                await _repositoryManager.SaveChanges();
                throw new LoginManyTimeException("Too many failed login attempts . Please try again later 5 minutes.");
            }
            if (foundCustomer.AccessFailedCount > 2)
            {
                _repositoryManager.Customer.UpdateCustomer(foundCustomer);
                await _repositoryManager.SaveChanges();
                throw new LoginManyTimeException($"Too many failed login attempts . Please try again later {foundCustomer.AccessFailedCount}/5 times.");
            }
            _repositoryManager.Customer.UpdateCustomer(foundCustomer);
            await _repositoryManager.SaveChanges();
            throw new WrongPasswordException("Wrong password please try again.");
        }
        if (!foundCustomer.ConfirmEmail)
        {
            _loggerManager.LogInfo("Service Authenticate: " + nameof(LoginAsync) + " Fail");
            throw new CustomerNotFoundException(loginDto.Email);
        }
        foundCustomer.LockoutEnd = null;
        foundCustomer.LockoutEnabled = false;
        foundCustomer.AccessFailedCount = 0;
        var result = _mapper.Map<ResponseCustomerDto>(foundCustomer);
        return result;
    }
    public async Task RemoveTokenCookie(long id ,HttpContext httpContext,bool trackChanges)
    {
        httpContext.Response.Cookies.Delete("access_token",new CookieOptions
        {
            HttpOnly = true,
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/"
        });
        httpContext.Response.Cookies.Delete("refresh_token",new CookieOptions
        {
            HttpOnly = true,
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/"
        });
        var customer = await _repositoryManager.Customer.GetCustomerByIdAsync(id, trackChanges);
        if (customer is null)
        {
            throw new CustomerNotFoundException("");
        }
        customer.RefreshToken = null;
        customer.RefreshTokenExpirytime = null;
        _repositoryManager.Customer.UpdateCustomer(customer);
        await _repositoryManager.SaveChanges();
    }
    public async Task<ResponseTokenDto> CreateTokenAsync(ResponseCustomerDto? customer,bool populateExp,bool remember,bool trackChanges)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:SecretKey").Value!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email,customer.Email!),
            new Claim(JwtRegisteredClaimNames.Name,customer.CustomerName!),
            new Claim(JwtRegisteredClaimNames.Sub,customer.CustomerId.ToString()!)
        };
        var tokenOptions = GenerateTokenOptions(credentials,claims);
        var refreshToken = GenerateRefreshToken();
        var foundCustomer = await _repositoryManager.Customer.GetCustomerByEmailAsync(customer.Email, trackChanges);
        if (foundCustomer is null)
        {
            throw new CustomerNotFoundException("Customer not exists");
        }
        foundCustomer.UpdatedAt = DateTime.UtcNow;
        foundCustomer.RefreshToken = refreshToken;
        if (populateExp)
        {
            foundCustomer.RefreshTokenExpirytime = DateTime.UtcNow.AddDays(remember ? 30 : 7);
        }
        _repositoryManager.Customer.UpdateCustomer(foundCustomer);
        await _repositoryManager.SaveChanges();
        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return new ResponseTokenDto(accessToken,refreshToken);
    }
    public async Task<RequestAuthenticateRegisterDto?> RegisterAsync(RequestAuthenticateRegisterDto registerDto)
    {
        var foundCustomer = await _repositoryManager.Customer.GetCustomerByEmailAsync(registerDto.Email, true);
        if (foundCustomer is not null && !foundCustomer.ConfirmEmail)
        {
            foundCustomer.UpdatedAt = DateTime.UtcNow;
            foundCustomer.Password = HashPassword(registerDto.Password);
            foundCustomer.Phone = registerDto.Phone;
            await _repositoryManager.SaveChanges();
            var foundCustomerDto = _mapper.Map<RequestAuthenticateRegisterDto>(foundCustomer);
            return foundCustomerDto;
        }
        if (await CheckEmailExists(registerDto.Email))
        {
            throw new EmailCustomerExistedException(registerDto.Email);
        }
        var customer = _mapper.Map<Customer>(registerDto);
        customer.Password = HashPassword(registerDto.Password);
        customer.CreatedAt = DateTime.UtcNow;
        await _repositoryManager.Customer.CreateAsync(customer);
        await _repositoryManager.SaveChanges();
        var customerCreated = _mapper.Map<RequestAuthenticateRegisterDto>(customer);
        _loggerManager.LogInfo("Service Authenticate: " + nameof(RegisterAsync) + " Success");
        return customerCreated;
    }
    public async Task<bool> ValidateTokenAsync(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:SecretKey").Value!));
        try
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true
            };
            var claims = await tokenHandler.ValidateTokenAsync(token,tokenValidationParameters);
            var customerIdClaim = claims
                .Claims[JwtRegisteredClaimNames.Sub].ToString();
            _loggerManager.LogWarn("Token does not contain a valid customer ID." + customerIdClaim);
            if (!string.IsNullOrWhiteSpace(customerIdClaim))
            {
                var customerId = long.Parse(customerIdClaim);
                var foundCustomer = await _repositoryManager.Customer.GetCustomerByIdAsync(customerId, false);
                if (foundCustomer is null)
                {
                    _loggerManager.LogWarn($"Customer with ID {customerId} not found.");
                    return false;
                }
                foundCustomer.UpdatedAt = DateTime.UtcNow;
                foundCustomer.ConfirmEmail = true;
                _repositoryManager.Customer.UpdateCustomer(foundCustomer);
                await _repositoryManager.SaveChanges();

                _loggerManager.LogInfo($"Customer with ID {customerId} successfully validated and updated.");
                return true;
            }
            else
            {
                _loggerManager.LogWarn("Token does not contain a valid customer ID.");
            }
        }
        catch (Exception ex)
        {
            _loggerManager.LogError($"Token validation failed: {ex.Message}");
        }
        return false;
    }
    public async Task<ResponseTokenDto> RefreshToken(RequestTokenDto requestTokenDto)
    {
        var tokenPrincipal = await GetPrincipalFromExpiredToken(requestTokenDto.Token);
        var customer = await _repositoryManager
            .Customer
            .GetCustomerByEmailAsync(tokenPrincipal.Claims[JwtRegisteredClaimNames.Email].ToString()!, false);
        if (customer is null || customer.RefreshToken != requestTokenDto.RefreshToken || customer.RefreshTokenExpirytime <= DateTime.UtcNow)
        {
            throw new RefreshTokenException(requestTokenDto.Token);
        }
        var customerDto = _mapper.Map<ResponseCustomerDto>(customer);
        return await CreateTokenAsync(customerDto, true,false,false);
    }
    public void SetTokenCookie(ResponseTokenDto tokenDto,HttpContext httpContext,bool remember)
    {
        httpContext.Response.Cookies.Append("access_token",tokenDto.Token, new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddMinutes(35),
            HttpOnly = true,
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/"
        });
        httpContext.Response.Cookies.Append("refresh_token",tokenDto.RefreshToken, new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(remember ? 30: 7),
            HttpOnly = true,
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/"
        });
    }
    private async Task<TokenValidationResult> GetPrincipalFromExpiredToken(string token)
    {
        var tokenSetting = _configuration.GetSection("Jwt");
        var tokenValidate = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSetting["SecretKey"]!));
        var tokenParameter = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = tokenValidate,
            ValidateLifetime = false,
            ValidIssuer = tokenSetting["Issuer"],
            ValidAudience = tokenSetting["Audience"]
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = await tokenHandler.ValidateTokenAsync(token,tokenParameter);
        var jwtToken = principal.SecurityToken as JwtSecurityToken;
        if (jwtToken is null ||
            !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }
        return principal;
    }
    private JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials,List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        Console.WriteLine(DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["expires"])));
        var tokenOptions = new JwtSecurityToken(
            claims:claims,
            issuer:jwtSettings["Issuer"],
            audience:jwtSettings["Audience"],
            expires:DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials:credentials);
        return tokenOptions;
    }
    private string GenerateRefreshToken()
    {
        var randomNumber = new Byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    private bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
    private async Task<bool> CheckEmailExists(string email)
    {
        _loggerManager.LogInfo("Service Authenticate: " + nameof(CheckEmailExists) + " Success");
        return await _repositoryManager.Customer.CheckEmailExistsAsync(email, false);
    }
    
    
}