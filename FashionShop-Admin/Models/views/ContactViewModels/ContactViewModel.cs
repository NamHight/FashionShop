using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models.views.ContactViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Please Enter FullName")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please Enter Description")]
        [StringLength(255)]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Please Enter Phone Number")]
        [StringLength(15)]
        [Phone(ErrorMessage = "Please Enter Valid Phone Number")]
        public string? Phone { get; set; }
        public string? Status { get; set; }
    }
}
