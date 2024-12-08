using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Controllers;

public class StoresController : Controller
{
    private readonly IManagerService _managerService;

    public StoresController(IManagerService _managerService)
    {
        this._managerService = _managerService;
    }
    // GET
    public async Task<IActionResult> Index(int page = 1, int limit = 10)
    {
        var stores = await _managerService.Store.GetAllPaginateAsync(page, limit, false);
        return View(stores);
    }

    public IActionResult Analysis()
    {
        return View();
    }
    public IActionResult Create()
    {
        return View();
    }
}