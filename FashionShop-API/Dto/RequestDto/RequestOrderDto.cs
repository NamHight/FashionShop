using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FashionShop_API.Dto.RequestDto
{
    public class RequestOrderDto
    {
        public decimal? TotalAmount { get; set; }

        [Column("payment_method", TypeName = "enum('cash','credit_card','debit_card','paypal')")]
        public string? PaymentMethod { get; set; }

        [Column("order_date", TypeName = "datetime")]
        public DateTime? OrderDate { get; set; }

        [Column("customer_id")]
        public long CustomerId { get; set; }

        [Column("reciver")]
        [StringLength(200)]
        public string? Reciver { get; set; }

        [Column("address")]
        [StringLength(200)]
        public string? Address { get; set; }

        [Column("phone")]
        [StringLength(15)]
        public string? Phone { get; set; }

        [JsonProperty]
        public IEnumerable<int> ListProductId = new List<int>();
    }
}
