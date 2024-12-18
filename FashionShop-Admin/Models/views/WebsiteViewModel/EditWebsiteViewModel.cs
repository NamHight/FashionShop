using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models.views.WebsiteViewModel;

public class EditWebsiteViewModel
{
    
    public int? WebsiteId { get; set; } 

    [Required(ErrorMessage = "Site name is required")]
    [StringLength(255, ErrorMessage = "Site name must be at most 255 characters long.")]
    public string? SiteName { get; set; } = null!;
    
    public string? Description { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; } = null!;

    [Required(ErrorMessage = "Phone is required")]
    [StringLength(15)]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string? Address { get; set; }
    
    [StringLength(255)]
    public string? Logo { get; set; }
    
    public string? Status { get; set; }
    
    public IFormFile? LogoFile { get; set; }
}