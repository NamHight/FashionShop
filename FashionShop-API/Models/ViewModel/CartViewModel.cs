using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FashionShop_API.Models.ViewModel
{
    public class CartViewModel
    {
        public long ProductId { get; set; }

        [Required(ErrorMessage ="Cột tên sản phẩm bắt buộc nhập")]
        [StringLength(255)]
        public string ProductName { get; set; } = null!;
        
        [StringLength(255)]
        public string? Banner { get; set; }

        public int Quantity { get; set; }   
 
        [Precision(10, 2)]
        public decimal? Price { get; set; }

        public decimal? Amount => Price * Quantity;

    }
}
