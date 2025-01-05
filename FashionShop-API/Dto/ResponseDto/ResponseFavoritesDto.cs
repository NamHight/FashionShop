using FashionShop_API.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FashionShop_API.Dto.ResponseDto
{
    public class ResponseFavoritesDto
    {
        public long FavoriteId { get; set; }
        public string? ProductName { get; set; }
        public string? Banner { get; set; }
        public decimal? Price { get; set; }
        public string? CategoryName { get; set; }
        public string? AddressStore { get; set; }
    }
}
