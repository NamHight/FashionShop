using FashionShop.Models;
using FashionShop.Models.views;
using FashionShop.Models.views.WebsiteViewModel;
using FashionShop.Repositories.ManagerRepository;

namespace FashionShop.Services.WebsiteInfos;

public class WebsiteService : IWebsiteService
{
    private readonly IManagerRepository _managerRepository;
    private readonly IWebHostEnvironment _hostEnvironment;
    public WebsiteService(IManagerRepository managerRepository,IWebHostEnvironment environment)
    {
        _managerRepository = managerRepository;
        _hostEnvironment = environment;
    }


    public async  Task<Dictionary<string,string>?> GetAllAsync(bool trackChanges)
    {
        try
        {
            var website = await _managerRepository.Website.GetWebsiteInfoAsync(trackChanges);
            if (website is null)
            {
                return null;
            }
            return website;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }
    
    public async Task<string> handleImage(IFormFile file, string directory, string[] allowedExtensions)
    {
        try
        {
            var wwwPath = _hostEnvironment.WebRootPath;
            var path = Path.Combine(wwwPath, directory);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var extension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException($"Only {string.Join(",",allowedExtensions)} extensions are allowed");
            }
            var fileName = $"{DateTime.Now.ToString("ddmmyyyy")}{extension}";
            var fullPath = Path.Combine(path, fileName);
            using var fileStream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return fileName;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<bool> UpdateAsync(WebsiteViewModel? websiteInfo, bool trackChanges)
    {
        try
        {
            if (websiteInfo is null)
            {
                return false;
            }
            if (websiteInfo.LogoFile != null)
            {
                if (!string.IsNullOrWhiteSpace(websiteInfo.Logo) && websiteInfo.Logo.Length > 1024 * 1024 * 1)
                {
                    throw new InvalidOperationException("Logo is too large");
                }
                var pathLogo = await handleImage(websiteInfo.LogoFile,"uploaded",new string[]{".png",".jpg",".jpeg"});
                websiteInfo.Logo = pathLogo;
            }

            var website = new Dictionary<int, string>();
            website.Add(1, websiteInfo.SiteName!);
            website.Add(3, websiteInfo.Email!);
            website.Add(4, websiteInfo.Phone!);
            website.Add(5, websiteInfo.Description!);
            website.Add(6, websiteInfo.Footer!);
            website.Add(7, websiteInfo.Address!);
            if (websiteInfo.Logo is not null)
            {
                website.Add( 2, websiteInfo.Logo);
            }
            foreach (var info in website)
            {
                var found = await _managerRepository.Website.GetWebsiteInfoByIdAsync(info.Key,trackChanges);
                if (found is null)
                {
                    return false;
                }
                found.WebisteValue = info.Value;
                await _managerRepository.Website.UpdateAsync(found);
                await _managerRepository.SaveAsync();
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

   public async Task<EditWebsiteViewModel> GetWebsiteInfoByIdAsync(int id, bool trackChanges)
    {
        try
        {
            var website = await _managerRepository.Website.GetWebsiteInfoByIdAsync(id, trackChanges);
            if (website is null)
            {
                return null;
            }

            var result = new EditWebsiteViewModel
            {
                // Address = website.Address,
                // Description = website.Description,
                // Email = website.Email,
                // Phone = website.Phone,
                // Status = website.Status,
                // WebsiteId = website.WebsiteId,
                // Logo = website.Logo,
                // SiteName = website.SiteName
            };
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
   
}