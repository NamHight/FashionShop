using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace FashionShop.Models.views.ReviewViewModels
{
    public class ReviewViewModel
    {
        public List<Review>? Reviews { get; set; }
        public List<Product>? Products { get; set; }
        public List<Customer>? Customers { get; set; }
        public PagingInfo? PagingInfo { get; set; }
    }
}
