using FashionShop_API.Models;

namespace FashionShop_API.Dto;

public class ResponseCategoryChildrenDto
{
    public string Name { get; set; }
    public IEnumerable<ResponseCategoryDto> Categories { get; set; }
}