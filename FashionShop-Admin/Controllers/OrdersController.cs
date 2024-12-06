

using FashionShop.Models.views.OrderDetails;
using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace FashionShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IManagerService managerService, ILogger<OrdersController> logger)
        {
            _managerService = managerService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _managerService.Orders.GetAllAsync(trackChanges: false);
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _managerService.Orders.GetByIdOrdersViewModel(id, false);
            return View(result);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateStatusPayment(long id, string payment)
        {
            var result = await _managerService.Orders.UpdateStatusPayMent(id, payment, true);
            return Json(new { status = 201, data = result, message = "Update payment thanh công" });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateStatus(long id, string status)
        {
            var result = await _managerService.Orders.UpdateStatus(id, status, true);
            return Json(new { status = 201, data = result, message = "Update status thanh công" });
        }

        [HttpDelete]
        public async Task<JsonResult> RemoveById(long id)
        {
            try
            {
                await _managerService.Orders.RemoveIdOrders(id, false);
                return Json(new { status = 201, message = "Xoá thành công" });
            }
            catch(Exception error)
            {
                Console.WriteLine("Error: " + error.Message);
                return Json(new { message = error.Message });
            } 
        }
    }
}
