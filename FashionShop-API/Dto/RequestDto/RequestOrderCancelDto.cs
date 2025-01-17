using System.ComponentModel.DataAnnotations;

namespace FashionShop_API.Dto.RequestDto
{
    public record RequestOrderCancelDto
    {
        [Required(ErrorMessage = "ID is not null")]
        public long? OrderId { get; init; }
        [Required(ErrorMessage = "ReasonCancel is not null")]
        public string? ReasonCancel { get; init; }
        public string? Status { get; init; }
    };
}
