using System.ComponentModel.DataAnnotations;
using FashionShop.Filters;

namespace FashionShop.Models.views.WebsiteViewModel;

public class CreateWebsiteViewModel
{
    [Required(ErrorMessage = "Site name is required")]
    [StringLength(255, ErrorMessage = "Site name must be at most 255 characters long.")]
    public string? SiteName { get; set; } = null!;
    
    public string? Description { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; } = null!;

    [Required(ErrorMessage = "Phone is required")]
    [StringLength(15)]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string? Address { get; set; }
    
    [StringLength(255)]
    public string? Logo { get; set; }
    
    public string? Status { get; set; }
    
    [MaxFileSize(1*1024*1024)]
    public IFormFile? LogoFile { get; set; }
}