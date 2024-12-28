using FashionShop_API.Dto;
using FashionShop_API.Dto.RequestDto;

namespace FashionShop_API.Services.Emails;

public interface IServiceEmail
{
    Task SendEmailConfirmAsync(RequestCustomerToken requestCustomerToken);
    Task<RequestCustomerToken?> HandleSendEmail(string email, bool trackChanges);
}