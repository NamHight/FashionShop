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
        public string? ProductName { get; set; }
        public string? ProductId {  get; set; }
        public string? Banner { get; set; }
        public decimal? Price { get; set; }
        public string? CategoryName { get; set; }
    }
}
