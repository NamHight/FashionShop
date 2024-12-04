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
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(long ContactId, string Status)
        {
             await _managerService.Contact.UpdateStatusAsync(ContactId, Status, trackChanges: true);
            
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _managerService.Contact.DeleteAsync(id, false);  
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
