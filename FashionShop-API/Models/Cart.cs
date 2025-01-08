using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FashionShop_API.Models
{
    public class Cart
    {
        public long ProductId { get; set; }

        [StringLength(255)]
        public string? Banner { get; set; }

        [Required(ErrorMessage = "Cột tên sản phẩm bắt buộc nhập")]
        [StringLength(255)]
        public string? ProductName { get; set; } = null!;

        [Precision(10, 2)]
        [Required(ErrorMessage = "Không được để trống giá sản phẩm")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0.")]
        public decimal? Price { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0.")]
        public int? Quantity { get; set; }

        public decimal? Amount => Price * Quantity;
    }
}
