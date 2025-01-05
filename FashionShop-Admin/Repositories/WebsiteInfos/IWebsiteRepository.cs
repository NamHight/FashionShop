using FashionShop.Models;

namespace FashionShop.Repositories.WebsiteInfos;

public interface IWebsiteRepository
{
    Task<Dictionary<string, string>?> GetWebsiteInfoAsync(bool trackChanges);

    Task<WebsiteInfo?> GetWebsiteInfoByIdAsync(int id, bool trackChanges);
    
    Task UpdateAsync(WebsiteInfo websiteInfo);
    
    Task CreateAsync(WebsiteInfo websiteInfo);
    
    Task DeleteAsync(WebsiteInfo websiteInfo);
}