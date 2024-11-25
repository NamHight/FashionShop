using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Models;

[Table("promotions")]
[Index("PromotionName", Name = "nq_promotion_name", IsUnique = true)]
[Index("Slug", Name = "slug", IsUnique = true)]
public partial class Promotion
{
    [Key]
    [Column("promotion_id")]
    public long PromotionId { get; set; }

    [Column("promotion_name")]
    public string PromotionName { get; set; } = null!;

    [Column("slug")]
    public string? Slug { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("start_date", TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column("end_date", TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [Column("discount_rate")]
    [Precision(10, 2)]
    public decimal? DiscountRate { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [Column("status", TypeName = "enum('active','expired')")]
    public string? Status { get; set; }
}
