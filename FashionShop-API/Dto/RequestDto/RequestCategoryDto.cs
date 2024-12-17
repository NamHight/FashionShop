using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FashionShop_API.Models.Enum;

namespace FashionShop_API.Dto.RequestDto;

public class RequestCategoryDto()
{
    [JsonIgnore]
    public long? CategoryId { get; set; }
    [Required(ErrorMessage = "CategoryName is required")]
    [StringLength(255, ErrorMessage = "CategoryName must be less than 255 characters")]
    [MinLength(8, ErrorMessage = "CategoryName must be at least 8 characters")]
    public string? CategoryName { get; set; }
    [Required(ErrorMessage = "Slug is required")]
    public string? Slug { get; set; }
    public string? Description { get; set; }
    [EnumDataType(typeof(StatusCategoryEnum),ErrorMessage = "Status must be active or inactive")]
    [Required(ErrorMessage = "Status is required")]
    public string? Status { get; set; }  
}
