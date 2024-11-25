using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Models;

[Table("images")]
[Index("ProductId", Name = "product_id")]
public partial class Image
{
    [Key]
    [Column("image_id")]
    public long ImageId { get; set; }

    [Column("product_id")]
    public long? ProductId { get; set; }

    [Column("url", TypeName = "text")]
    public string Url { get; set; } = null!;

    [Column("alt_text")]
    [StringLength(255)]
    public string? AltText { get; set; }

    [Column("type", TypeName = "enum('thumbnail','gallery','primary')")]
    public string? Type { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Images")]
    public virtual Product? Product { get; set; }
}
