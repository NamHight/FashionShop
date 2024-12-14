using System.Net;
using System.Net.Mail;
using System.Text;
using FashionShop.Models;
using FashionShop.Models.views;
using FashionShop.Models.views.EmployeeViewModels;
using FashionShop.Repositories.ManagerRepository;
using Microsoft.Extensions.Options;

namespace FashionShop.Services.Emails;

public class EmailService : IEmailService
{
    private const string templatePath = @"Template/{0}.html";
    private readonly SMTPConfigModel _smtpConfigModel;
    private readonly IConfiguration _configuration;
    private readonly IManagerRepository _managerRepository;
    public EmailService(IManagerRepository managerRepository,IOptions<SMTPConfigModel> smtpConfigModel,IConfiguration configuration)
    {
        _managerRepository = managerRepository;
        _smtpConfigModel = smtpConfigModel.Value;
        _configuration = configuration;
    }

    private async Task SendEmailForForgotPassword(UserEmailOption model)
    {
        model.Subject = "Reset Password Email";
        model.Body = UpdatePlaceHodler(GetEmailTemplate("ForgotPassword"),model.MyPropertys);
        await SendEmailAsync(model);
    }

    private string UpdatePlaceHodler(string text, List<KeyValuePair<string, string>> placeHolders)
    {
        if (!string.IsNullOrEmpty(text) && placeHolders != null)
        {
            foreach (var item in placeHolders)
            {
                if (text.Contains(item.Key))
                {
                    text = text.Replace(item.Key, item.Value);
                }
            }
        }

        return text;
    }
    public string TokenGenerate()
    {
        return Guid.NewGuid().ToString("N");
    }
    public async Task GeneratorForgotPassword()
    {
        var token = TokenGenerate();
        
    }
    public async Task SendEmailForgotPasswordEmail(EmployeeWithToken user)
    {
        string appDomain = _configuration.GetSection("Application:Domain").Value;
        string confirmLink = _configuration.GetSection("Application:ForgotPassword").Value;
        var userOption = new UserEmailOption
        {
            ToEmails = new List<string>() { user.Employee.Email },
            MyPropertys = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("{{Link}}",string.Format(appDomain+confirmLink,user.token,user.Employee.EmployeeId))   
            }
        };
        await SendEmailForForgotPassword(userOption);
    }
    private async Task SendEmailAsync(UserEmailOption model)
    {
        MailMessage mail = new MailMessage
        {
            Subject = model.Subject,
            Body = model.Body,
            From = new MailAddress(_smtpConfigModel.SenderAddress, _smtpConfigModel.SenderDisplayName),
            IsBodyHtml = _smtpConfigModel.IsBodyHtml
        };
        foreach (var toEmail in model.ToEmails)
        {
            mail.To.Add(toEmail);
        }
        NetworkCredential networkCredential = new NetworkCredential(_smtpConfigModel.UserName, _smtpConfigModel.Password);
        SmtpClient smtpClient = new SmtpClient
        {
            Host = _smtpConfigModel.Host,
            Port = _smtpConfigModel.Port,
            EnableSsl = _smtpConfigModel.EnableSSL,
            Credentials = networkCredential
        };
        mail.BodyEncoding = Encoding.Default;
        await smtpClient.SendMailAsync(mail);
    }

    private string GetEmailTemplate(string templateName)
    {
        var body = File.ReadAllText(string.Format(templatePath,templateName));
        return body;
    }
}