using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FashionShop_API.Dto.RequestDto
{
    public class RequestCartDto
    {
        public long ProductId { get; set; }
        public int? Quantity { get; set; }

        public string? Banner { get; set; }
       
        public decimal? Price { get; set; }

        public decimal? Amount { get; set; }
    }
}
