using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models.views.RoleViewModels;

public class CreateRoleViewModel
{
    
    [Required(ErrorMessage = "Role name is required")]
    public string? RoleName { get; set; }

    
    public string? Description { get; set; }

    [Required(ErrorMessage = "Status is required")]
    public string? Status { get; set; }
}