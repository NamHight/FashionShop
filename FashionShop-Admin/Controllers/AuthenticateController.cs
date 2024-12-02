using System.Security.Claims;
using FashionShop.Models.views;
using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Controllers;

public class AuthenticateController : Controller
{
    private readonly IManagerService _managerService;

    public AuthenticateController(IManagerService managerService)
    {
        _managerService = managerService;
    }
    [HttpGet]
    public async Task<IActionResult> Login()
    {
  
        return View("Login");
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        if (ModelState.IsValid)
        {
            if (login.Email != null && login.Password != null)
            {
                var employee = await _managerService.Employee.LoginAsync(login.Email, login.Password, false);
                if (employee != null)
                {
                    if (employee.Email != null && employee.EmployeeName != null && employee.Role != null)
                    {
                        var clams = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, employee.EmployeeName),
                            new Claim(ClaimTypes.NameIdentifier,employee.EmployeeId.ToString()),
                            new Claim(ClaimTypes.Email,employee.Email),
                            new Claim(ClaimTypes.Role,employee.Role.RoleName)
                        };
                        var claimsIdentity = new ClaimsIdentity(clams,CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,claimsPrincipal);
                    }

                    return RedirectToAction("Index", "Dashboard");
                }
                ModelState.AddModelError("", "Email or Password is incorrect");
            }
        }
        return View("Login",login);
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Authenticate");
    }
}