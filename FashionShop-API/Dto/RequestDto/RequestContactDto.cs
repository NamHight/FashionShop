using System.ComponentModel.DataAnnotations;

namespace FashionShop_API.Dto.RequestDto
{
    public class RequestContactDto
    {
        [Required(ErrorMessage ="FullName is Required!")]
        [StringLength(255, ErrorMessage ="FullName must be less than 255 characters")]
        public string? Fullname { get; set; }
        [Required(ErrorMessage ="Email is Required!")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Description is Required!")]
        [StringLength(255, ErrorMessage = "FullName must be less than 255 characters")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Phone is Required!")]
        [StringLength(10, ErrorMessage ="Phone must be equal than 10 characters")]
        public string? Phone { get; set; }
    }
}
