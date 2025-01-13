namespace FashionShop_API.Dto.ResponseDto
{
    public record SuppilerDto(long SupplierId,  string SupplierName, string ContactName, string Phone, string? Email);
}
