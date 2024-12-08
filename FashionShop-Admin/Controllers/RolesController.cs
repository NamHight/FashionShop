using FashionShop.Models.Enum;
using FashionShop.Models.views.RoleViewModels;
using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Controllers;
[Authorize]
public class RolesController : Controller
{
    private readonly IManagerService _managerService;
    public RolesController(IManagerService managerService)
    {
        _managerService = managerService;
    }

    public async Task<IActionResult> Index(int page = 1, int limit = 10)
    {

        var roles = await _managerService.Role.GetAllPaginateAsync(page, limit, false);
        return View(roles);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _managerService.Role.CreateAsync(model);
        if (result)
        {
            TempData[nameof(NotificationTypes.Success)] = "Create role successfully.";
            return RedirectToAction("Index");
        }
        TempData[nameof(NotificationTypes.Error)] = "Create role failed.";
        return View(model);
    }

    public async Task<IActionResult> CheckUniqueRoleName(string roleName)
    {
        var result = await _managerService.Role.CheckUniqueName(roleName,false);
        if (result)
        {
            return Json(new { success = true, message = "Role name available." });
        }
        return Json(new { success = false, message = "Role name already exists." });
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        var role = await _managerService.Role.GetByIdAsync(id, false);
        return View(role);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id,CreateRoleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var result = await _managerService.Role.UpdateAsync(id, model, false);
        if (!result)
        {
            TempData[nameof(NotificationTypes.Error)] = "Update role failed.";
            return View(model);
        }
        TempData[nameof(NotificationTypes.Success)] = "Update role successfully.";
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _managerService.Role.DeleteAsync(id, false);
        if (!result)
        {
            TempData[nameof(NotificationTypes.Error)] = "Delete role failed.";
            return RedirectToAction("Index");
        }
        TempData[nameof(NotificationTypes.Success)] = "Delete role successfully.";
        return RedirectToAction("Index");
    }
}