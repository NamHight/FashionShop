namespace FashionShop.Models.views;

public class SidebarItem
{
    public string? title { get; set; }
    public string? url { get; set; }
    public string? icon { get; set; }
    private List<SidebarItem>? _subItems;
    public List<SidebarItem> subItems => _subItems ??= new List<SidebarItem>(); 
    public bool hasSubItems => subItems.Any();
}