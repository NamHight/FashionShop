using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionShop_API.Dto.RequestDto
{
	public class RequestReviewDto
	{
		public long ReviewId { get; set; }

		[Required(ErrorMessage = "Hãy nhập số sao")]
		[Range(1, 5, ErrorMessage = "Số sao phải trong khoảng từ 1 đến 5.")]
		public sbyte? Rating { get; set; }

		[Required(ErrorMessage = "Hãy miêu tả về sản phẩm")]
		[StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
		public string? ReviewText { get; set; }

		public DateTime? ReviewDate { get; set; }

		public long? CustomerId { get; set; }
		public long? ProductId { get; set; }


	}
}
