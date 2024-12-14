using System.Security.Claims;
using FashionShop.Models.views;
using FashionShop.Services.ManagerService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
                        if (!string.IsNullOrEmpty(employee.Avatar))
                        {
                            clams.Add(new Claim("AvatarUrl",employee.Avatar));
                        }
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

    [AllowAnonymous]
    public async Task<IActionResult> ForgetPassword()
    {
        return View(new ForgotPasswordModel());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ForgetPassword(ForgotPasswordModel model)
    {
        if (ModelState.IsValid)
        {
            ModelState.Clear();
            model.EmailSent = true;
            var result = await _managerService.Employee.handleSendEmail(model.Email, false);
            if (result != null)
            {
                await _managerService.Email.SendEmailForgotPasswordEmail(result); 
            }

            return View(model);
        }
        return View(model);
    }

    public async Task<IActionResult> ResetPassword(long id , string token)
    {
        return View(new ResetPassword{IsSuccess = false, employeeId = id, token = token});
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPassword model)
    {
        if (ModelState.IsValid)
        {
            ModelState.Clear();
            var employee = await _managerService.Employee.confirmTokenAsync(model.employeeId, model.token);
            if (employee)
            {
                await _managerService.Employee.ResetPassword(model, true);
            }
            return RedirectToAction("Login");
        }
        return View(model);
    }
}