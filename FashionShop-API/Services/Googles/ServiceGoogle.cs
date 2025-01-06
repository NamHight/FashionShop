using AutoMapper;
using FashionShop_API.Dto;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;
using FashionShop_API.Repositories;
using FashionShop_API.Services.ServiceLogger;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;

namespace FashionShop_API.Services.Googles;
public class ServiceGoogle : IServiceGoogle
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly GoogleOption _googleOption;
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;
    public ServiceGoogle(IRepositoryManager repositoryManager, IOptions<GoogleOption> googleOption, ILoggerManager loggerManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _mapper = mapper;
        _googleOption = googleOption.Value;
    }

    public async Task<ResponseCustomerDto?> GoogleSignIn(GoogleSignVM googleSignVM)
    {
        GoogleJsonWebSignature.Payload payload;
        try
        {
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(googleSignVM.Token, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _googleOption.ClientId }
                });
            }
            catch (Exception e)
            {
                _loggerManager.LogError($"Google Token validation failed: {e.Message}");
                return null;
            }
            var customer = await _repositoryManager.Customer.GetCustomerByEmailAsync(payload.Email, false)
                           ?? await _repositoryManager.Customer.GetCustomerByGoogleIdAsync(payload.Subject, false);
            if (customer is not null)
            {
                var customerDto = _mapper.Map<ResponseCustomerDto>(customer);
                return customerDto;
            }

            var customerCreate = new Customer
            {
                Email = payload.Email,
                Avatar = payload.Picture,
                CustomerName = payload.Name,
                CreatedAt = DateTime.UtcNow,
                LoginProvider = "GOOGLE",
                ConfirmEmail = true,
                GoogleId = payload.Subject
            };
            await _repositoryManager.Customer.CreateAsync(customerCreate);
            var result = await _repositoryManager.SaveChangesAsync();
            if (!result)
            {
                return null;
            }
            var foundCustomer = await _repositoryManager.Customer.GetCustomerByGoogleIdAsync(customerCreate.GoogleId, false);
            var responseCustomerDto  = _mapper.Map<ResponseCustomerDto>(foundCustomer);
            return responseCustomerDto;
        }
        catch (Exception e)
        {
            throw new Exception("GoogleSignIn: " + e.Message);
        }
    }
    
    
}