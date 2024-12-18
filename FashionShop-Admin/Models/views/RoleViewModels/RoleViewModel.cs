namespace FashionShop.Models.views.RoleViewModels;

public class RoleViewModel
{
    public IEnumerable<Role> Roles { get; set; }
    
    public PagingInfo PagingInfo { get; set; }
}