using FashionShop_API.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FashionShop_API.Dto.ResponseDto
{
    public class ResponseOrderDetailsDto
    {
        public long OrderDetailId { get; set; }

        public int? Quantity { get; set; }

        public decimal? TotalPrice { get; set; }
        public long? OrderId { get; set; }
        public long? ProductId { get; set; }
        public string? Status { get; set; }
    }
}
