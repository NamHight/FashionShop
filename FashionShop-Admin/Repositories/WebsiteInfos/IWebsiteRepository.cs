using FashionShop.Models;

namespace FashionShop.Repositories.WebsiteInfos;

public interface IWebsiteRepository
{
    Task<IEnumerable<WebsiteInfo>> GetAllPaginateAsync(int page, int limit, bool trackChanges);
    Task<int> CountAsync();

    Task<WebsiteInfo> GetWebsiteInfoByIdAsync(int id, bool trackChanges);
    
    Task UpdateAsync(WebsiteInfo websiteInfo);
    
    Task CreateAsync(WebsiteInfo websiteInfo);
    
    Task<bool> CheckUniqueName(string name,bool trackChanges);
    Task<bool> CheckUniqueEmail(string email,bool trackChanges);
    Task DeleteAsync(WebsiteInfo websiteInfo);
}