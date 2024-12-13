using FashionShop.Context;
using FashionShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Repositories.WebsiteInfos;

public class WebsiteRepository : GenericRepo<WebsiteInfo>, IWebsiteRepository
{
    public WebsiteRepository(MyDbContext context) : base(context)
    {
    }


    public async Task<IEnumerable<WebsiteInfo>> GetAllPaginateAsync(int page, int limit, bool trackChanges)
    {

        var website = await FindAll(trackChanges)
            .OrderByDescending(website => website.CreatedAt)
            .ThenBy(website => website.Status)
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();
        return website;
    }

    public async Task<int> CountAsync()
    {
        var count = await Count();
        return count;
    }

    public async Task<WebsiteInfo> GetWebsiteInfoByIdAsync(int id, bool trackChanges)
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

    public async Task<bool> CheckUniqueName(string name, bool trackChanges)
    {
        var web = await FindById(website => website.SiteName.Equals(name), trackChanges).FirstOrDefaultAsync();
        return web != null;
    }

    public async Task<bool> CheckUniqueEmail(string email, bool trackChanges)
    {
        var web = await FindById(web => web.Email.Equals(email), trackChanges).FirstOrDefaultAsync();
        return web != null;
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