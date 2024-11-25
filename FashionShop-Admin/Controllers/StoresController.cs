using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Controllers;

public class StoresController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("Index");
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