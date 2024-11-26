using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(IManagerService managerService, ILogger<ContactsController> logger)
        {
            _managerService = managerService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _managerService.Contact.GetAllAsync(trackChanges: false);
            return View(result);
        }
    }
}
