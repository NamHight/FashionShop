using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Models;

[Table("brands")]
[Index("BrandName", Name = "brand_name", IsUnique = true)]
[Index("Slug", Name = "slug", IsUnique = true)]
public partial class Brand
{
    [Key]
    [Column("brand_id")]
    public long BrandId { get; set; }

    [Column("slug")]
    public string? Slug { get; set; }

    [Column("brand_name")]
    public string? BrandName { get; set; }

    [Column("description")]
    [StringLength(255)]
    public string? Description { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime UpdatedAt { get; set; }

    [Column("status", TypeName = "enum('available','unavailable')")]
    public string? Status { get; set; }
}
