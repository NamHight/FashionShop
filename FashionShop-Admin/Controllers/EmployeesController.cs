using FashionShop.Models;
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
    public async Task<IActionResult> Create([FromBody]CreateEmployeeViewModel employee)
    {
        if (!ModelState.IsValid)
        {
            _logger.Log(LogLevel.Information,"Đã vào đây");
            return BadRequest(ModelState);
        }
        _logger.Log(LogLevel.Information,"Tạo thành công");

        await _managerService.Employee.CreateAsync(employee);
        return Ok(new {Status = 200, Message = "Thêm nhân viên thành công!"});
    }
}