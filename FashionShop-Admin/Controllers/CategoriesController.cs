using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Controllers;

public class CategoriesController : Controller
{
    private readonly IManagerService _managerService;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(IManagerService managerService, ILogger<CategoriesController> logger)
    {
        _managerService = managerService;
        _logger = logger;
    }
    public async Task<IActionResult> Index()
    {
        var result = await _managerService.Category.GetAllAsync(trackChanges:false);
        return View(result); 
    }
}