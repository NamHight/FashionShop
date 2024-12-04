using System.ComponentModel.DataAnnotations;
using FashionShop.Filters;
using FashionShop.Models.Enum;

namespace FashionShop.Models.views;

public class EditEmployeeViewModel
{
    public long? EmployeeId { get; set; }
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "Please Enter Employee Name")]
    [StringLength(255)]
    public string? EmployeeName { get; set; }
     
    [Required(ErrorMessage = "Please Enter Phone Number")]
    [StringLength(15)]
    [Phone(ErrorMessage = "Please Enter Valid Phone Number")]
    public string? Phone { get; set; }
    
    [Required(ErrorMessage = "Please Enter Employee Position")]
    public string? EmployeePosition { get; set; }
    public string? Status { get; set; }
    public string? Gender { get; set; }
     
    public string? Description { get; set; }
    
    public string? Address { get; set; }
    [Required(ErrorMessage = "Please Enter Birth Date")]
    [DataType(DataType.Date,ErrorMessage = "Please Enter Valid Date")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] //dd/MM/yyyy
    public DateOnly Birth { get; set; }
    
    [Required(ErrorMessage = "Please Select Role")]
    public int? RoleId { get; set; }
    public long? StoreId { get; set; }
    
    public string? Avatar { get; set; }
    
    [MaxFileSize(1 * 1024 * 1024)]
    public IFormFile? AvatarFile { get; set; }
    
    public string? RoleName { get; set; }
    public string? StoreName { get; set; }

    public List<string>? EmployeePositions { get; set; } = System.Enum.GetNames(typeof(EmployeePositionEnum)).ToList();
    public List<string>? EmployeeStatus { get; set; } = System.Enum.GetNames(typeof(EmployeeStatus)).ToList();
    public IEnumerable<Role>? Roles { get; set; }
    public IEnumerable<Store>? Stores { get; set; }


}