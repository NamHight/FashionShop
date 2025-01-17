
using System.Text.Json.Serialization;
using FashionShop_API.Dto.QueryParam;

namespace FashionShop_API.Dto.RequestDto
{
    public class RequestProductDto : ParamProductDto
    {

        public string? searchProduct { get; set; }
        public string? categoryName { get; set; }

        public decimal? minPrice { get; set; } = 0;
        public decimal? maxPrice { get; set; } = decimal.MaxValue;
    }
}
