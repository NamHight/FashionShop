using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionShop_API.Dto.RequestDto
{
    public class RequestOrderDetailDto
    {
        [Column("quantity")]
        public int? Quantity { get; set; }

        [Column("total_price")]
        [Precision(10, 2)]
        public decimal? TotalPrice { get; set; }

        [Column("order_id")]
        public long? OrderId { get; set; }

        [Column("product_id")]
        public long? ProductId { get; set; }

        [Column("status", TypeName = "enum('pending','fulfilled')")]
        public string? Status { get; set; }
    }
}
