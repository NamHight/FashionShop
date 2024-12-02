using FashionShop.Models.Enum;
using FashionShop.Models.views;
using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Controllers;

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
}