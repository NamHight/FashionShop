using FashionShop.Models;
using FashionShop.Models.views.WebsiteViewModel;

namespace FashionShop.Services.WebsiteInfos;

public interface IWebsiteService
{
    Task<WebsiteViewModel> GetAllPaginateAsync(int page,int limit,bool trackChanges);
    Task<bool> ChangeStatusAsync(int id, string status, bool trackChanges);
    Task<bool> UpdateAsync(int id, EditWebsiteViewModel websiteInfo, bool trackChanges);
    Task<EditWebsiteViewModel> GetWebsiteInfoByIdAsync(int id, bool trackChanges);
    Task<bool> CreateAsync(CreateWebsiteViewModel websiteInfo);
    Task<bool> CheckUniqueName(string name, bool trackChanges);
    Task<bool> CheckUniqueEmail(string email, bool trackChanges);
    Task<bool> DeleteAsync(int id, bool trackChanges);
}