namespace FashionShop.Models.views;

public class SMTPConfigModel
{
    public string SenderAddress { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string SenderDisplayName { get; set; }
    public string UserName { get; set; }
    public bool EnableSSL { get; set; }
    public bool UseDefaultCredentials { get; set; }
    public bool IsBodyHtml { get; set; }
}