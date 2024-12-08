using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FashionShop.Models;
using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Authorization;

namespace FashionShop.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly IManagerService _managerService;
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(ILogger<DashboardController> logger,IManagerService managerService)
    {
        _logger = logger;
        _managerService = managerService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}