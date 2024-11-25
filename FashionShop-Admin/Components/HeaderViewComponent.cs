using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Components;

public class HeaderViewComponent : ViewComponent
{
    public HeaderViewComponent()
    {
        
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {

        
        return View();
    }
    
    
}