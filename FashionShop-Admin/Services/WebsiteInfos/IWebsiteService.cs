using FashionShop.Models;
using FashionShop.Models.views.WebsiteViewModel;

namespace FashionShop.Services.WebsiteInfos;

public interface IWebsiteService
{
    Task<Dictionary<string,string>?> GetAllAsync(bool trackChanges);
    Task<bool> UpdateAsync(WebsiteViewModel? websiteInfo, bool trackChanges);
    Task<EditWebsiteViewModel> GetWebsiteInfoByIdAsync(int id, bool trackChanges);
}