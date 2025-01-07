namespace FashionShop_API.Dto.RequestDto;

public record RequestLoginGoogle(string? Email, string? LoginProvider, string? Gender, string? Avatar);