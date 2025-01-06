namespace FashionShop_API.Dto;

public class ResponseCustomerDto
{
    public long? CustomerId { get; set; }
    public string? CustomerName { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Avatar { get; set; }
    public DateOnly? Birth { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }
    public bool ConfirmEmail { get; set; }
    public string? Status { get; set; }
    public string? LoginProvider { get; set; }
}