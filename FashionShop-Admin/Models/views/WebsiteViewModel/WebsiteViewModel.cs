using System.ComponentModel.DataAnnotations;
using FashionShop.Filters;

namespace FashionShop.Models.views.WebsiteViewModel;

public class WebsiteViewModel
{
    public Dictionary<string,string>? WebsiteInfo { get; set; }
    
    [Required(ErrorMessage = "Site name is required")]
    [StringLength(255, ErrorMessage = "Site name must be at most 255 characters long.")]
    public string? SiteName { get; set; } = null!;
    public string? Description { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; } = null!;
    [Required(ErrorMessage = "Phone is required")]
    [StringLength(15)]
    [MinLength(10, ErrorMessage = "Phone number must be at least 10 digits long")]
    [MaxLength(15, ErrorMessage = "Phone number must be at most 15 digits long")]
    [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must only contain numbers and cannot be negative.")]
    public string? Phone { get; set; }
    [Required(ErrorMessage = "Address is required")]
    public string? Address { get; set; }
    [StringLength(255)]
    public string? Logo { get; set; }
    [Required(ErrorMessage = "Logo is required")]
    public string? Footer { get; set; }
    [MaxFileSize(1 * 1024 * 1024)]
    public IFormFile? LogoFile { get; set; }
}