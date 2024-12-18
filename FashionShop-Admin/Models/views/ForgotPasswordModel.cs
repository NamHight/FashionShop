using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models.views;

public class ForgotPasswordModel
{
    [Required(ErrorMessage = "Please enter your email")]
    [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please Enter Valid Email Address")]
    public string? Email { get; set; }
    
    public bool? EmailSent { get; set; }
}