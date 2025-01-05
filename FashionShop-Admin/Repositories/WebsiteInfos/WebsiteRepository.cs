using FashionShop.Context;
using FashionShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Repositories.WebsiteInfos;

public class WebsiteRepository : GenericRepo<WebsiteInfo>, IWebsiteRepository
{
    public WebsiteRepository(MyDbContext context) : base(context)
    {
    }


    public async Task<Dictionary<string,string>?> GetWebsiteInfoAsync(bool trackChanges)
    {
        var website = await FindAll(trackChanges).ToDictionaryAsync(w => w.WebsiteKey, w => w.WebisteValue);
        return website;
    }
    public async Task<WebsiteInfo?> GetWebsiteInfoByIdAsync(int id, bool trackChanges)
    {
        var website = await FindById(item => item.WebsiteId.Equals(id), trackChanges).FirstOrDefaultAsync();
        return website;
    }
    public async Task UpdateAsync(WebsiteInfo websiteInfo)
    {
        try
        {
            Update(websiteInfo);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task CreateAsync(WebsiteInfo websiteInfo)
    {
        try
        {
            Create(websiteInfo);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DeleteAsync(WebsiteInfo websiteInfo)
    {
        try
        {
            Delete(websiteInfo);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}