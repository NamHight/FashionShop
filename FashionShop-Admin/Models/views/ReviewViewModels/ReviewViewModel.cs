using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace FashionShop.Models.views.ReviewViewModels
{
    public class ReviewViewModel
    {
        public IEnumerable<Review>? Reviews { get; set; }
        public string? NameSearch { get; set; }
        public PagingInfo? PagingInfo { get; set; }
    }
}
