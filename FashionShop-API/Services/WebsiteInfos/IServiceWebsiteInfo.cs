namespace FashionShop_API.Services.WebsiteInfos;

public interface IServiceWebsiteInfo
{
    Task<Dictionary<string, string>> GetWebsiteInfoAsync(bool trackChanges);
}