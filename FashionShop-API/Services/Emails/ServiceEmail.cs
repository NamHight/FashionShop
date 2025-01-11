using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using FashionShop_API.Dto;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Options;
using FashionShop_API.Repositories;
using FashionShop_API.Services.ServiceLogger;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace FashionShop_API.Services.Emails;

public class ServiceEmail : IServiceEmail
{
    private const string templatePath = @"Templates/{0}.html";
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly GmailOption _gmailOption;
    private readonly IConfiguration _configuration;
    public ServiceEmail(ILoggerManager loggerManager,IOptions<GmailOption> gmailOption,IRepositoryManager repositoryManager, IConfiguration configuration)
    {
        _loggerManager = loggerManager;
        _repositoryManager = repositoryManager;
        _configuration = configuration;
        _gmailOption = gmailOption.Value;
    }
    public async Task SendEmailConfirmAsync(RequestCustomerToken requestCustomerToken,string tempUrl,string template)
    {
        string? domain = _configuration.GetSection("Application:Domain").Value;
        string? url = @"api/Authenticate/{1}?token={0}";
        var responseEmail = new ResponseGmailDto
        {
            Recipient = requestCustomerToken.Customer.Email,
            Attachments = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("{{Link}}",string.Format(domain+url,requestCustomerToken.Token,tempUrl)),
                new KeyValuePair<string, string>("{{Name}}",requestCustomerToken.Customer.CustomerName)
            }
        };
        await SendEmailAsyncWithTemplate("Confirm Email", responseEmail,template);
    }

    public async Task<RequestCustomerToken?> HandleSendEmail(string email, bool trackChanges)
    {
        var customer = await _repositoryManager.Customer.GetCustomerByEmailAsync(email, trackChanges);
        if (customer is null)
        {
            return null;
        }
        var token = _generateToken(customer.CustomerId,DateTime.UtcNow.AddMinutes(5));
        var result = new RequestCustomerToken(customer, token);
        return result;
    }
    private string _generateToken(long customerId,DateTime expires)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:SecretKey").Value!));
        var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
        var issuer = _configuration.GetSection("Jwt:Issuer").Value;
        var audience = _configuration.GetSection("Jwt:Audience").Value;
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub,customerId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        };
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience:audience,
            claims:claims,
            expires:expires,
            signingCredentials:credentials
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private async Task SendEmailAsyncWithTemplate(string title,ResponseGmailDto model,string template)
    {
        model.Subject = title;
        model.Body = UpdatePlaceHolder(GetEmailTemplate(template), model.Attachments);
        await SendMailAsync(model);
    }
    private string UpdatePlaceHolder(string text, List<KeyValuePair<string, string>>? placeHolder)
    {
        if (!string.IsNullOrWhiteSpace(text) && placeHolder is not null)
        {
            foreach (var item in placeHolder)
            {
                if (text.Contains(item.Key))
                {
                    text = text.Replace(item.Key, item.Value);
                }
            }
        }

        return text;
    }
    private async Task SendMailAsync(ResponseGmailDto responseGmailDto)
    {
        _loggerManager.LogInfo("Service Email: " + nameof(SendMailAsync) + " Success");
        MailMessage mailMessage = new MailMessage
        {
            From = new MailAddress(_gmailOption.Email),
            Subject = responseGmailDto.Subject,
            Body = responseGmailDto.Body,
            IsBodyHtml = true
        };
        mailMessage.To.Add(new MailAddress(responseGmailDto.Recipient));
        using var smtpClient = new SmtpClient();
        smtpClient.Host = _gmailOption.Host;
        smtpClient.Port = _gmailOption.Port;
        smtpClient.Credentials = new NetworkCredential(_gmailOption.Email, _gmailOption.Password);
        smtpClient.EnableSsl = true;
        await smtpClient.SendMailAsync(mailMessage);
    }

    private string GetEmailTemplate(string nameTemplate)
    {
        var template = File.ReadAllText(string.Format(templatePath, nameTemplate));
        return template;
    }
    
}