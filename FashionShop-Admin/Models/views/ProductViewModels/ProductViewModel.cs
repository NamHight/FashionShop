using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models
{
    public class ProductViewModel
    {
        public List<Product>? Products { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
