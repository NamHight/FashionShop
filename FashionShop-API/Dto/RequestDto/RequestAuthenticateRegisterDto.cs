using System.ComponentModel.DataAnnotations;

namespace FashionShop_API.Dto.RequestDto;

public class RequestAuthenticateRegisterDto
{

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password, ErrorMessage = "Invalid password")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one letter and one number.")]
        [MinLength(6, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Password and confirm password does not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [StringLength(255, ErrorMessage = "Customer name must be less than 255 characters")]
        [MinLength(5, ErrorMessage = "Customer name must be at least 5 characters")]
        public string? CustomerName { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [Required(ErrorMessage = "Phone is required")]
        [MinLength(10, ErrorMessage = "Phone number must be at least 10 characters")]
        [MaxLength(15, ErrorMessage = "Phone number must be at most 15 characters")]
        [RegularExpression(@"^09\d*$", ErrorMessage = "Phone number must start with '09' and contain only digits.")]
        public string? Phone { get; set; }
}