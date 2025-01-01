namespace FashionShop_API.Dto;

public class ResponseGmailDto
{
    public string Recipient { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<KeyValuePair<string,string>> Attachments { get; set; }
}