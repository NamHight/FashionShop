using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models.views;

public class ResetPassword
{
    public long? employeeId { get; set; }
    public string? token { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [StringLength(40, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 40 characters")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    [Required(ErrorMessage = "Confirm password is required")]
    [Compare("Password",ErrorMessage = "Password and confirm password does not match")]
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }

    public bool? IsSuccess { get; set; } = false;
}