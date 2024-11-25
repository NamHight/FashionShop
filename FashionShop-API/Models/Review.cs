using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Models;

[Table("reviews")]
[Index("CustomerId", Name = "fk_reviews_customer")]
[Index("ProductId", Name = "fk_reviews_product")]
public partial class Review
{
    [Key]
    [Column("review_id")]
    public long ReviewId { get; set; }

    [Column("rating")]
    public sbyte? Rating { get; set; }

    [Column("review_text", TypeName = "text")]
    public string? ReviewText { get; set; }

    [Column("review_date", TypeName = "datetime")]
    public DateTime? ReviewDate { get; set; }

    [Column("product_id")]
    public long? ProductId { get; set; }

    [Column("customer_id")]
    public long? CustomerId { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [Column("status", TypeName = "enum('approved','pending','rejected')")]
    public string? Status { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Reviews")]
    public virtual Customer? Customer { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Reviews")]
    public virtual Product? Product { get; set; }
}
