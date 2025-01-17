
using System.Text.Json.Serialization;
using FashionShop_API.Dto.QueryParam;

namespace FashionShop_API.Dto.RequestDto
{
    public class RequestProductDto : ParamProductDto
    {
        public decimal? MinPrice { get; set; } = 0;
        public decimal? MaxPrice { get; set; } = decimal.MaxValue;
        public string? CategoryName { get; set; }

       public string? SearchProduct { get; set; }
    }
}
