namespace FashionShop_API.Repositories.WebsiteInfos;

public interface IRepositoryWebsiteInfo
{
    Task<Dictionary<string, string>> GetWebsiteInfoAsync(bool trackChanges);
}