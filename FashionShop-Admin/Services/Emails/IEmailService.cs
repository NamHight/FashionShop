using FashionShop.Models;
using FashionShop.Models.views;
using FashionShop.Models.views.EmployeeViewModels;

namespace FashionShop.Services.Emails;

public interface IEmailService
{
    Task SendEmailForgotPasswordEmail(EmployeeWithToken user);
}