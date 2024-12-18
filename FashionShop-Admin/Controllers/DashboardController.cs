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

    public DashboardController(ILogger<DashboardController> logger, IManagerService managerService)
    {
        _logger = logger;
        _managerService = managerService;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    public async Task<IActionResult> GetPurchaseStatic()
    {
        var result = await _managerService.Dashboard.GetListSalesDataByMonthInYear(DateTime.Now.Year, false);
        return Json(Ok(new { data = result, message = "Get purchase static successfully" }));
    }

    public async Task<IActionResult> GetRevenuaPrice()
    {
        var result = await _managerService.Dashboard.GetListRevenuaDataByMonthInYear(DateTime.Now.Year, false);
        return Json(Ok(new { data = result, message = "Get purchase static successfully" }));
    }

    public async Task<IActionResult> GetCustomerStatus()
    {
        var result = await _managerService.Dashboard.GetListStatusCustomer();
        return Json(Ok(new { data = result }));
    }

    public async Task<IActionResult> GetDataAllStatic(DateTime date)
    {
        var result = await _managerService.Dashboard.GetDataAllStatic(date, false);
        return Json(Ok(new { data = result, message = "Get data all static successfully" }));
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

 
	public async Task<IActionResult> getCountProductPerMonth()
	{
        var result = await _managerService.Dashboard.getCountProductPerMonth();
		return Json(new { data = result, message = "Get data all static successfully"});
	}

}