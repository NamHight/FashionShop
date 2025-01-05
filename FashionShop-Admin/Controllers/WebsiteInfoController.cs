using FashionShop.Models.Enum;
using FashionShop.Models.views.WebsiteViewModel;
using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace FashionShop.Controllers;
[Authorize]
public class WebsiteInfoController : Controller
{
    private readonly IManagerService _managerService;

    public WebsiteInfoController(IManagerService managerService)
    {
        _managerService = managerService;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var websiteInfo = await _managerService.Website.GetAllAsync( false) ?? new Dictionary<string, string>(); 
        var webiste = new WebsiteViewModel
        {
            WebsiteInfo = websiteInfo,
        };
        return View(webiste);
    }
    [HttpPost]
    public async Task<IActionResult> Index(WebsiteViewModel websiteInfo)
    {
        if (!ModelState.IsValid)
        {
            var websiteInfoDictionary = await _managerService.Website.GetAllAsync( false) ?? new Dictionary<string, string>(); 
            websiteInfo.WebsiteInfo = websiteInfoDictionary;
            return View(websiteInfo);
        }
        var result = await _managerService.Website.UpdateAsync(websiteInfo, false);
        if (!result)
        {
            var websiteInfoDictionary = await _managerService.Website.GetAllAsync( false) ?? new Dictionary<string, string>(); 
            websiteInfo.WebsiteInfo = websiteInfoDictionary;
            TempData[nameof(NotificationTypes.Error)] = "Update failed.";
            return View(websiteInfo);
        }
        TempData[nameof(NotificationTypes.Success)] = "Update successfully.";
        return RedirectToAction("Index");
    }
    
}