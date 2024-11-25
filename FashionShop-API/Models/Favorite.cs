using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Models;

[Table("favorites")]
[Index("CustomerId", Name = "fk_favorites_customer")]
[Index("ProductId", Name = "fk_favorites_product")]
public partial class Favorite
{
    [Key]
    [Column("favorite_id")]
    public long FavoriteId { get; set; }

    [Column("product_id")]
    public long? ProductId { get; set; }

    [Column("customer_id")]
    public long? CustomerId { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [Column("status", TypeName = "enum('active','inactive')")]
    public string? Status { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Favorites")]
    public virtual Customer? Customer { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Favorites")]
    public virtual Product? Product { get; set; }
}
