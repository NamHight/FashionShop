using FashionShop.Models;
using FashionShop.Models.views.ContactViewModels;
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
       
        public async Task<IActionResult> Edit(long id)
        {
            var contact= await _managerService.Contact.GetByIdAsync(id, trackChanges: false);
            if (contact == null)
            {
                return NotFound();
            }
            ViewBag.Status = new List<string>
                {
                    "pending",
                    "resovle",
                    "importain"
                };
            var contactViewModel = new ContactViewModel
            {
                FullName = contact.Fullname,
                Email = contact.Email,
                Description = contact.Description,
                Phone = contact.Phone,
                Status = contact.Status,
            };
            return View(contactViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(long id, ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var contact = await _managerService.Contact.GetByIdAsync(id, trackChanges: false);
                if (contact == null)
                {
                    return NotFound();
                }
                contact.Fullname = model.FullName;
                contact.Email = model.Email;
                contact.Description = model.Description;
                contact.Phone = model.Phone;
                contact.Status = model.Status;
                await _managerService.Contact.EditAsync(contact,false);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
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
