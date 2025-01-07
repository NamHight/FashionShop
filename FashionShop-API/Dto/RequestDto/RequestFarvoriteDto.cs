using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionShop_API.Dto.RequestDto
{
	public class RequestFarvoriteDto
	{
		public long ProductId { get; set; }
		public long CustomerId { get; set; }
		[Column("status", TypeName = "enum('active','inactive')")]
		public string? Status { get; set; }

	}
}
