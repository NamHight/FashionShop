using System.Drawing.Printing;
using FashionShop.Filters;
using FashionShop.Models;
using FashionShop.Models.views.ReviewViewModels;
using FashionShop.Models.views;
using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting.Internal;

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
    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var categoryPages = await _managerService.Category.GetPageLinkAsync(page, pageSize, trackChanges: false);
        var categories = await _managerService.Category.GetAllAsync(trackChanges: false);
        //var result = await _managerService.Category.GetAllAsync(trackChanges:false);
        var result = new CategoryViewModel
        {
            Categories = categoryPages,
            PagingInfo = new PagingInfo
            {
                TotalItems = categories.Count(),
                ItemsPerPage = pageSize,
                CurrentPage = page,
            }
        };
        return View(result); 
    }
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<JsonResult> CheckSlug(string slug)
    {
        if (await _managerService.Category.CheckSlug(slug))
        {
            return Json(new { statusCode = 0, slugg = slug });
        }
        return Json(new { statusCode = 1, slugg = slug });
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category collection)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var category = new Category();
                category = collection;
                await _managerService.Category.AddNewCategory(category);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                       
                        Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }
                return View();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
            return View();
        }
    }
    public ActionResult Edit(int id)
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
    // POST: ProductsController/Delete/5
    [HttpPost]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _managerService.Category.DeleteAsync(id, false);
        if (result)
        {
            return RedirectToAction(nameof(Index));
        }
        else
        {
            return NotFound();
        }
    }
    [HttpPost]
    public async Task<IActionResult> ChangeStatus(long CategoryId, string Status)
    {
        await _managerService.Category.UpdateStatusAsync(CategoryId, Status, trackChanges: true);

        return RedirectToAction(nameof(Index));
    }
}