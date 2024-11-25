using FashionShop.Models.views;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Components;

public class SidebarViewComponent : ViewComponent
{
    private static readonly List<SidebarItem> _sidebarItems = new List<SidebarItem>
    {
        new SidebarItem { title = "Dashboard", url = "/Dashboard", icon = "icofont-home" },
        new SidebarItem{ title = "Categories",url="/Categories", icon = "icofont-folder" },
        new SidebarItem 
        { 
            title = "Stores", url = "/Stores", icon = "icofont-brand-appstore",
            subItems =
            {
                new SidebarItem { title = "List Stores", url = "/Stores" },
                new SidebarItem { title = "Analysis Stores", url = "/Stores/Analysis" }
            }
        },
        new SidebarItem
        {
            title = "Orders", url = "/Orders", icon = "icofont-notepad",
            subItems=
            {
                new SidebarItem { title = "List Orders", url = "/Orders" },
                new SidebarItem { title = "Analysis Orders", url = "/Orders/Analysis" }
            }
        },
        new SidebarItem
        {
            title="Products",url="/Products", icon = "icofont-truck-loaded",
            subItems =
            {
                new SidebarItem {title = "List Products", url="/Products"},
                new SidebarItem {title = "Analysis Products", url="/Products/Analysis"}
            }
        },
        
    };
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var routeValue = ViewContext.RouteData.Values["controller"];
        ViewBag.selectedMenu = routeValue;
        return View(_sidebarItems);
    }
    
}