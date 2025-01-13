using System.ComponentModel.DataAnnotations;

namespace FashionShop_API.Dto.RequestDto;

public class RequestSearchProductDto
{
    [Required(ErrorMessage = "Search term is required")]
    public string searchTerm { get; set; }
    [Required(ErrorMessage = "Sort order is required")]
    public string sortOrder { get; set; }
}