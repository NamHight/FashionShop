using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FashionShop.Models.views;

namespace FashionShop.Models
{
    public class CategoryViewModel
    {
        public string? NameSearch { get; set; }
        public PagingInfo? PagingInfo { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
