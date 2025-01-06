using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FashionShop_API.Dto.RequestDto
{
    public class RequestCartDto
    {
        public long ProductId { get; set; }
        public string? Banner { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }
    }
}
