using FashionShop_API.Models;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionShop_API.Dto.ResponseDto
{
    public class ResponseOrdersDto()
    {
        public long OrderId { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public List<ResponseOrderDetailsDto> Ordersdetails { get; set; }
    };
}
