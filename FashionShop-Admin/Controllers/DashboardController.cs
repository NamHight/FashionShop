using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FashionShop.Models;
using Microsoft.AspNetCore.Authorization;
using FashionShop.Services.ManagerService;

namespace FashionShop.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly ILogger<DashboardController> _logger;
    private readonly IManagerService _managerService;

    public DashboardController(ILogger<DashboardController> logger, IManagerService managerService)
    {
        _logger = logger;
        _managerService = managerService;
    }

    public IActionResult Index()
    {
        var countProduct;//How much products sold?
        var countProductbyid;//How much products by id sold?
        var countTotailRevenue;// What is the total revenue?
        ViewBag.CountProduct = countProduct;

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