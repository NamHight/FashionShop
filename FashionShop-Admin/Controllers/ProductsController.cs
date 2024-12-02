using FashionShop.Models;
using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace FashionShop.Controllers
{
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
        public async Task<IActionResult> Index()
        {
            var resutl = new ProductViewModel()
            {
                Products = await _managerService.Product.GetAllAsync(trackChanges: false),
                Categories = await _managerService.Category.GetAllAsync(trackChanges: false)
            };
            return View(resutl);
        }
        // GET: ProductsController/Details/5

        // GET: ProductsController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> CheckSlug(string slug)
        {
            if(await _managerService.Product.CheckSlug(slug))
            {
                return Json(new { statusCode = 0, slugg = slug});
            }
            return Json(new { statusCode = 1, slugg = slug });
        }
        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  Create(Product collection, IFormFile Banner)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var product = new Product();
                    product = collection; // gán trị trị của đối tượng nhận từ model bindding cho product
                    if (Banner != null && Banner.Length > 0) // xử lý lưu ảnh nếu ảnh tồn tại
                    {
                        // Lưu file: hostingEnvironment.WebRootPath = localhost:3000/wwwroot
                        var fullpath = Path.Combine(hostingEnvironment.WebRootPath, "uploaded"); // fullpath = localhost:3000/wwwroot/updloaded
                        using (var stream = new FileStream(Path.Combine(fullpath,Banner.FileName), FileMode.Create))
                        {
                            await Banner.CopyToAsync(stream); // lưu ảnh vào localhost:3000/wwwroot/updloaded/fileName
                        }
                        product.Banner = Banner.FileName; // lưu tên ảnh vào database
                    }
                    await _managerService.Product.AddNewProduct(product);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var key in ModelState.Keys) // in ra danh sách lỗi nếu như ModelState.IsValid = false
                    {
                        var errors = ModelState[key].Errors;
                        foreach (var error in errors)
                        {
                            // Log hoặc hiển thị lỗi
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

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductsController/Edit/5
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

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController/Delete/5
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
