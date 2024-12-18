using System.ComponentModel.DataAnnotations;
namespace FashionShop.Models.views.CategoryViewModels
{
    public class UpdateCategoryViewModel
    {
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Không được để trống tên Danh Mục")]
        [StringLength(100)]
        public string? CategoryName { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
