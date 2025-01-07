using System.ComponentModel.DataAnnotations;

namespace FashionShop_API.Dto.ResponseDto;

public class GoogleSignVM
{
    [Required]
    public string Token { get; set; }
}