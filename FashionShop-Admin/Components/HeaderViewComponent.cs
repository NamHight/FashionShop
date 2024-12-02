using System.Security.Claims;
using FashionShop.Models.views;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Components;

public class HeaderViewComponent : ViewComponent
{
    public HeaderViewComponent()
    {
        
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var claimsPrincipal = (ClaimsPrincipal)User;
        var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
        var role = claimsPrincipal.FindFirstValue(ClaimTypes.Role);
        var headerModel = new HeaderViewModel
        {
            Name = claimsPrincipal.Identity.Name,
            Email = email,
            Role = role
        };
        
        return View(headerModel);
    }
    
    
}