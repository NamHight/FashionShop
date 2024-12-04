using FashionShop.Models.Enum;
using FashionShop.Models.views;
using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace FashionShop.Controllers;

[Authorize(Roles = "Admin")]
public class EmployeesController : Controller
{
    private readonly IManagerService _managerService;
    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(IManagerService managerService, ILogger<EmployeesController> logger)
    {
        _managerService = managerService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var employees = await _managerService.Employee.GetAllAsync(false);
        return View(employees);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var employees = new CreateEmployeeViewModel
        {
            Roles = await _managerService.Role.GetAllAsync(false),
            Stores = await _managerService.Store.GetAllAsync(false)
        };

        return View(employees);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateEmployeeViewModel model)
    {
        _logger.Log(LogLevel.Information, "Vào phương thưc post Create");
        _logger.Log(LogLevel.Information, ModelState.IsValid.ToString());
        if (!ModelState.IsValid)
        {
            _logger.Log(LogLevel.Warning, "Đã vào else");
            model.Roles = await _managerService.Role.GetAllAsync(false);
            model.Stores = await _managerService.Store.GetAllAsync(false);
            TempData[nameof(NotificationTypes.Error)] = "Create failed.";
            return View(model);
        }
        try
        {
            if (model.AvatarFile != null)
            {
                if (model.Avatar != null && model.Avatar.Length > 1 * 1024 * 1024)
                {
                    throw new InvalidOperationException("Avatar must be less than 1MB");
                }

                model.Avatar = await _managerService
                    .Employee
                    .HandleSaveFileAsync(model.AvatarFile, "uploaded",
                        new string[] { ".png", ".jpg", ".jpeg" });
            }
            await _managerService.Employee.CreateAsync(model);
            TempData[nameof(NotificationTypes.Success)] = "Create successfully.";
            return RedirectToAction("Index", "Employees");
        }
        catch (InvalidOperationException e)
        {
            TempData[nameof(NotificationTypes.Error)] = e;
            return View(model);
        }
        catch (FileNotFoundException e)
        {
            TempData[nameof(NotificationTypes.Error)] = e;
            return View(model);
        }
        catch (Exception e)
        {
            TempData[nameof(NotificationTypes.Error)] = e;
            return View(model);
        }
    }

    public async Task<IActionResult> CheckUniqueEmail(string email)
    {
        var check = await _managerService.Employee.CheckUniqueEmail(email, false);
        if (check)
        {
            return Json(Ok(new {message = "Email is already exists"}));
        }
        return Json(Accepted(new {message = "Email is available"}));
    }

    public async Task<IActionResult> Edit(long id)
    {
        var employee = await GetEmployeeWithRolesAndStores(id);
        return View(employee);
    }

    

    [HttpPost]
    public async Task<IActionResult> Edit(long id, EditEmployeeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var employee = await GetEmployeeWithRolesAndStores(id);
            TempData[nameof(NotificationTypes.Error)] = "Update failed.";
            return View(employee);
        }
        var result=  await _managerService.Employee.UpdateAsync(id,model,false);
        if (result)
        {
            TempData[nameof(NotificationTypes.Success)] = "Update successfully.";
            return RedirectToAction("Edit"); 
        } 
        var updateEmployee = await GetEmployeeWithRolesAndStores(id);
        TempData[nameof(NotificationTypes.Warning)] = "Nothing to Update.";
        return View(updateEmployee);
    }
    private async Task<EditEmployeeViewModel> GetEmployeeWithRolesAndStores(long id)
    {
        var employee = await _managerService.Employee.GetByIdEditAsync(id, false);
        if (employee != null)
        {
            employee.Roles = await _managerService.Role.GetAllAsync(false);
            employee.Stores = await _managerService.Store.GetAllAsync(false);
        }
        return employee;
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(long EmployeeId, ChangePasswordModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(m => m.Value.Errors.Count > 0)
                .ToDictionary(m => m.Key, m => m.Value.Errors.Select(e => e.ErrorMessage))
                .ToArray();
            return Json(new { success = false, errors });
        }
        var result = await _managerService.Employee.ChangePassword(model, EmployeeId, false);
        if (result)
        {
            return Json(new {success = true, message = "Change password successfully."});
        }
        return Json(new {success = false, message = "Change password failed."});
    }

    public async Task<IActionResult> CheckPassword(long id, string password)
    {
        var result = await _managerService.Employee.CheckPassword(id, password, false);
        return Json(new {Message = "test"});
    }
}