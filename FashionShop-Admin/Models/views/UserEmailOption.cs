namespace FashionShop.Models.views;

public class  UserEmailOption
{
    public List<string> ToEmails { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<KeyValuePair<string,string>> MyPropertys { get; set; }
}