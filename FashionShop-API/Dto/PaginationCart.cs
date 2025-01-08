using FashionShop_API.Models;

namespace FashionShop_API.Dto
{
    public class PaginationCart
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; } 
    }
}
