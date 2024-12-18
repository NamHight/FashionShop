﻿using System.Drawing.Printing;
using FashionShop.Filters;
using FashionShop.Models;
using FashionShop.Models.views.ReviewViewModels;
using FashionShop.Models.views;
using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting.Internal;
using FashionShop.Models.views.CategoryViewModels;
using FashionShop.Models.views.ProductViewModels;
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
    public async Task<IActionResult> Index(string nameSearch, int page = 1, int pageSize = 10)
    {
        var result = await _managerService.Category.GetPageLinkAsync(page, pageSize, nameSearch, trackChanges: false);
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
    public async Task<ActionResult> Edit(int id)
    {
        try
        {
            var category = await _managerService.Category.GetByIdAsync(id, false);
            var resutl = new UpdateCategoryViewModel()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Slug = category.Slug,
                Description = category.Description,
                Status = category.Status,
            };
            return View(resutl);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Loi la {ex.Message}");
            throw;
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, UpdateCategoryViewModel collection)
    {
        try
        {
            var a= await _managerService.Category.UpdateCategoryAsync(id, collection,true);
            if(a==true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(collection);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Loi khong xac dinh {ex}");
            throw;
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