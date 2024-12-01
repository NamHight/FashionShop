using System.ComponentModel.DataAnnotations;
using FashionShop.Models.Enum;

namespace FashionShop.Models.views;

public class CreateEmployeeViewModel
{
    [Required(ErrorMessage = "Please Enter Email Address")]
    [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please Enter Valid Email Address")]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "Please Enter Password")]
    [StringLength(40, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 40 characters")]
    [Compare("ConfirmPassword",ErrorMessage = "Password and confirm password does not match")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    
    [DataType(DataType.ImageUrl)]
    public string? Avatar { get; set; }
    
    [EnumDataType(typeof(EmployeeStatus),ErrorMessage = "Please Enter Valid Status")]
    public string? Status { get; set; }
    
    public string? Gender { get; set; }
    public long? StoreId { get; set; }
    
    [Required(ErrorMessage = "Please Select Role")]
    public int RoleId { get; set; }
    
    [Required(ErrorMessage = "Please Enter Confirm Password")]
    [Compare("Password",ErrorMessage = "Password and confirm password does not match")]
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }
    
    [Required(ErrorMessage = "Please Enter Phone Number")]
    [StringLength(15)]
    [Phone(ErrorMessage = "Please Enter Valid Phone Number")]
    public string? Phone { get; set; }
    
    [Required(ErrorMessage = "Please Enter Employee Name")]
    [StringLength(255)]
    public string? EmployeeName { get; set; }

    [Required(ErrorMessage = "Please Enter Employee Position")]
    [EnumDataType(typeof(EmployeePosition))]
    public string? EmployeePosition { get; set; }

    public IEnumerable<Role?> Roles { get; set; }

    public IEnumerable<Store?> Stores { get; set; } 

}