namespace FashionShop_API.Options;

public class GmailOption
{
    public const string GmailOptionKey = "EmailSettings";
    public string Host { get; set; }
    public int Port { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}