using FashionShop.Models;
using FashionShop.Models.views.ProductViewModels;
using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting.Internal;
using System.Reflection;

namespace FashionShop.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private IWebHostEnvironment hostingEnvironment; //xử lý đường dẫn ảnh
        private readonly IManagerService _managerService; // sử dụng các hàm trong service
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IManagerService managerService, ILogger<ProductsController> logger, IWebHostEnvironment environment)
        {
            _managerService = managerService;
            _logger = logger;
            hostingEnvironment = environment;
        }
        public async Task<IActionResult> Index(string nameSearch, int page = 1, int pageSize = 10)
        {
            var result = await _managerService.Product.GetPageLinkAsync(nameSearch, page, pageSize, trackChanges: false);
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            var resutl = new UpdateProductViewModel()
            {
                Categories = await _managerService.Category.GetAllAsync(false)
            };
            return View(resutl);
        }

        [HttpPost]
        public async Task<JsonResult> CheckSlug(string slug)
        {
            if (await _managerService.Product.CheckSlug(slug))
            {
                return Json(new { statusCode = 0, slugg = slug });
            }
            return Json(new { statusCode = 1, slugg = slug });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateCategoryId(string newData, long idProduct)
        {
            var newCategoryID = await _managerService.Category.FindByNameAsync(newData);
            // lấy id category dựa theo name_category mới cập nhật.

            // cho phép trackChanges nên chỉ cần gán lại thuộc tính cần update, k cần gọi hàm update
            if (await _managerService.Product.UpdateCategoryId(newCategoryID, idProduct, false))
            {
                // nếu cập nhật thành công thì trả về statusCode = 1 ngược lại trả về 0
                return Json(new { statusCode = 1, newCategory = newData, idProduct = idProduct, id_category = newCategoryID });
            }
            return Json(new { statusCode = 0, newCategory = newData, idProduct = idProduct, id_category = newCategoryID });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateStatus(string newData, long idProduct)
        {
            if (await _managerService.Product.UpdateStatus(newData, idProduct, false))
            {
                return Json(new { statusCode = 0, newStatus = newData });
            }
            return Json(new { statusCode = 1, newStatus = newData });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UpdateProductViewModel collection)
        {
            if (ModelState.IsValid)
            {
                var fileName = await _managerService.Product.HandleUploadImage(hostingEnvironment, collection.Banner);
                var product = new Product()
                {
                    ProductName = collection.ProductName ?? "null",
                    Slug = collection.Slug ?? "null",
                    Status = collection.Status,
                    Description = collection.Description ?? "null",
                    Banner = fileName,
                    CategoryId = await _managerService.Category.FindByNameAsync(collection.CategoryName ?? "null"),
                    Price = collection.Price,
                };
                // lưu đối tượng product vào csdl
                await _managerService.Product.AddNewProduct(product);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var key in ModelState.Keys) // in ra danh sách lỗi nếu như ModelState.IsValid = false
                {
                    var errors = ModelState[key]?.Errors;
                    foreach (var error in errors)
                    {
                        // Log hoặc hiển thị lỗi
                        Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }
                var resutl = new UpdateProductViewModel()
                {
                    Categories = await _managerService.Category.GetAllAsync(false)
                };
                return View(resutl);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var product = await _managerService.Product.GetByIdAsync(id, false);
                var resutl = new UpdateProductViewModel()
                {
                    ProductId = product!.ProductId,
                    ProductName = product.ProductName,
                    Slug = product.Slug,
                    Status = product.Status,
                    Description = product.Description,
                    BannerUrl = product.Banner,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                    Categories = await _managerService.Category.GetAllAsync(false)
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
        public async Task<ActionResult> Edit(int id, UpdateProductViewModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Console.WriteLine("Sai rang buoc");
                    collection.Categories = await _managerService.Category.GetAllAsync(false);
                    return View("Edit", collection);
                }
                else
                {
                    var image = await _managerService.Product.HandleUploadImage(hostingEnvironment, collection.Banner);
                    if (await _managerService.Product.UpdateProductAsync(id, collection, image))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        Console.WriteLine("Truy van loi");
                        collection.Categories = await _managerService.Category.GetAllAsync(false);
                        return View(collection);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi khong xac dinh {ex}");
                throw;
            }
        }


        public ActionResult Delete(int id)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
