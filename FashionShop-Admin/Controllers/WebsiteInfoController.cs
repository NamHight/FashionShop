using FashionShop.Models.Enum;
using FashionShop.Models.views.WebsiteViewModel;
using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace FashionShop.Controllers;

public class WebsiteInfoController : Controller
{
    private readonly IManagerService _managerService;

    public WebsiteInfoController(IManagerService managerService)
    {
        _managerService = managerService;
    }
    // GET
    public async Task<IActionResult> Index(int page = 1, int limit = 10)
    {
        var websiteInfo = await _managerService.Website.GetAllPaginateAsync(page, limit, false);
        return View(websiteInfo);
    }
    [HttpPost]
    public async Task<IActionResult> ChangeStatus(int id, string status)
    {
        var result = await _managerService.Website.ChangeStatusAsync(id, status, true);
        if (!result)
        {
            return Json(BadRequest(new {message = "Update status failed"}));
        }
        return Json(Accepted(new {message = "Update status successfully"}));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var website = await _managerService.Website.GetWebsiteInfoByIdAsync(id, false);
        return View(website);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id,EditWebsiteViewModel websiteInfo)
    {
        if (!ModelState.IsValid)
        {
            return View(websiteInfo);
        }
        var result = await _managerService.Website.UpdateAsync(id, websiteInfo, false);
        if (!result)
        {
            TempData[nameof(NotificationTypes.Error)] = "Update failed.";
            return View(websiteInfo);
        }
        TempData[nameof(NotificationTypes.Success)] = "Update successfully.";
        return RedirectToAction("Edit");
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWebsiteViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _managerService.Website.CreateAsync(model);
        if (!result)
        {
            TempData[nameof(NotificationTypes.Error)] = "Create failed.";
            return View(model);
        }
        TempData[nameof(NotificationTypes.Success)] = "Create successfully.";
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> CheckUniqueName(string name)
    {
        var result = await _managerService.Website.CheckUniqueName(name,false);
        if (result)
        {
            return Json(BadRequest(new {message = "Name already exists."}));
        }
        return Json(Accepted(new {message = "Name available."}));
    }
    [HttpPost]
    public async Task<IActionResult> CheckUniqueEmail(string email)
    {
        var result = await _managerService.Website.CheckUniqueEmail(email,false);
        if (result)
        {
            return Json(BadRequest(new {message = "Email already exists."}));
        }
        return Json(Accepted(new {message = "Email available."}));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _managerService.Website.DeleteAsync(id, false);
        if (!result)
        {
            TempData[nameof(NotificationTypes.Error)] = "Delete failed.";
            return RedirectToAction("Index");
        }
        TempData[nameof(NotificationTypes.Success)] = "Delete successfully.";
        return RedirectToAction("Index");
    }
}