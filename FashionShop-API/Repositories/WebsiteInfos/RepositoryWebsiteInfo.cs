using FashionShop_API.Context;
using FashionShop_API.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Repositories.WebsiteInfos;

public class RepositoryWebsiteInfo : RepositoryBase<WebsiteInfo>, IRepositoryWebsiteInfo
{
    public RepositoryWebsiteInfo(MyDbContext context) : base(context)
    {
    }
    
    public async Task<Dictionary<string ,string>> GetWebsiteInfoAsync(bool trackChanges) => await FindAll(trackChanges).ToDictionaryAsync(e => e.WebsiteKey, e => e.WebisteValue);
}