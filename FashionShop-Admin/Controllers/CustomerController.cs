using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Controllers;

public class CustomerController : Controller
{
    private readonly IManagerService _managerService;

    public CustomerController(IManagerService managerService)
    {
        _managerService = managerService;
    }
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> GetCountCustomer()
    {
        var result = await _managerService.Customer.CountAsync();
        return Json(Ok(new {data = result, message = "Count customer successfully"}));
    }
}