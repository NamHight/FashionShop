using FashionShop_API.Models;

namespace FashionShop_API.Dto.RequestDto;

public record RequestCustomerToken(Customer Customer, string Token);