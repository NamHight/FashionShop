using System.ComponentModel.DataAnnotations;

namespace FashionShop_API.Dto.RequestDto;

public class RequestResetPasswordDto
{
    [Required(ErrorMessage = "Token is required")]
    public string token { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one letter and one number.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string password { get; set; }
    
}