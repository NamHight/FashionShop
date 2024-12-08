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


    public async Task<WebsiteViewModel> GetAllPaginateAsync(int page, int limit, bool trackChanges)
    {
        var website = await _managerRepository.Website.GetAllPaginateAsync(page, limit, trackChanges);
        var count = await _managerRepository.Website.CountAsync();
        var result = new WebsiteViewModel
        {
            WebsiteInfos = website,
            PagingInfo = new PagingInfo
            {
                TotalItems = count,
                CurrentPage = page,
                ItemsPerPage = limit
            }
        };

        return result;
    }

    public async Task<bool> ChangeStatusAsync(int id,string status, bool trackChanges)
    {
        try
        {
            var website = await _managerRepository.Website.GetWebsiteInfoByIdAsync(id, trackChanges);
            if (website is null)
            {
                return false;
            }

            website.Status = status;
            await _managerRepository.SaveAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
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
    public async Task<bool> UpdateAsync(int id, EditWebsiteViewModel websiteInfo, bool trackChanges)
    {
        try
        {
            var website = await _managerRepository.Website.GetWebsiteInfoByIdAsync(id, trackChanges);
            if (website is null)
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
                website.Logo = pathLogo;
            }
            website.Address = websiteInfo.Address;
            website.Description = websiteInfo.Description;
            website.Email = websiteInfo.Email;
            website.Phone = websiteInfo.Phone;
            website.Status = websiteInfo.Status;
            website.SiteName = websiteInfo.SiteName;
            website.UpdateAt = DateTime.Now;
            await _managerRepository.Website.UpdateAsync(website);
            await _managerRepository.SaveAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
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
                Address = website.Address,
                Description = website.Description,
                Email = website.Email,
                Phone = website.Phone,
                Status = website.Status,
                WebsiteId = website.WebsiteId,
                Logo = website.Logo,
                SiteName = website.SiteName
            };
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> CreateAsync(CreateWebsiteViewModel websiteInfo)
    {
        try
        {
            if (websiteInfo is null)
            {
                return false;
            }

           
            var website = new WebsiteInfo
            {
                Address = websiteInfo.Address,
                Description = websiteInfo.Description,
                Email = websiteInfo.Email,
                Phone = websiteInfo.Phone,
                Status = websiteInfo.Status,
                SiteName = websiteInfo.SiteName,
                CreatedAt = DateTime.Now
            };
            if (websiteInfo.LogoFile != null)
            {
                if (websiteInfo.Logo != null && websiteInfo.Logo.Length > 1024 * 1024 * 1)
                {
                    throw new InvalidOperationException("Logo is too large");
                }

                website.Logo = await handleImage(websiteInfo.LogoFile, "uploaded",
                    new string[] { ".png", ".jpg", ".jpeg" });
            }
            await _managerRepository.Website.CreateAsync(website);
            await _managerRepository.SaveAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> CheckUniqueName(string name, bool trackChanges)
    {
        try
        {
            var result = await _managerRepository.Website.CheckUniqueName(name, trackChanges);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> CheckUniqueEmail(string email, bool trackChanges)
    {
        try
        {
            var result = await _managerRepository.Website.CheckUniqueEmail(email, trackChanges);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, bool trackChanges)
    {
        try
        {
            var website = await _managerRepository.Website.GetWebsiteInfoByIdAsync(id, trackChanges);
            if (website is null)
            {
                return false;
            }
            await _managerRepository.Website.DeleteAsync(website);
            await _managerRepository.SaveAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}