using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FashionShop_API.Dto.ResponseDto
{
    public class ResponseSearchProductDto
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Banner { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? CategoryName { get; set; }
    }
}
