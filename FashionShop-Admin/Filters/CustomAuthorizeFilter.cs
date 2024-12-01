
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FashionShop.Filters;

public class CustomAuthorizeFilter : Attribute, IAuthorizationFilter
{

    private readonly string _role;
    public CustomAuthorizeFilter(string role)
    {
        this._role = role;  
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (user.Identity != null && !user.Identity.IsAuthenticated)
        {
            context.Result = new RedirectToActionResult("Login","Authenticate",null);
            return;
        }

        if (!user.IsInRole(_role))
        {
            context.Result = new ForbidResult();
        }
    }
}