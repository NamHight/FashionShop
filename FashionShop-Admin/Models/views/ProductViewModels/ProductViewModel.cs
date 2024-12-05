using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models
{
    public class ProductViewModel
    {
        // chứa 2 danh sách product và category để sử dụng trong view danh sách sản phẩm
        public List<Product>? Products { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
