using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionShop.Models.views.ProductViewModels
{
    public class UpdateProductViewModel
    {
        public long ProductId { get; set; }

        [Required(ErrorMessage = "Không được để trống tên sản phẩm")]
        [StringLength(100)]
        public string? ProductName { get; set; }

        public string? Slug { get; set; }

        public string? BannerUrl { get; set; }

        public IFormFile? Banner { get; set; }

        public string? Description { get; set; }

      
        [Precision(10, 2)]
        [Required(ErrorMessage = "Không được để trống giá sản phẩm")]
        public decimal? Price { get; set; }
        public long? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Status { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
