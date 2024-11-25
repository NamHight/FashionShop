using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Models;

[Table("cartsdetail")]
[Index("CartId", Name = "fk_cartsdetail_cart")]
[Index("ProductId", Name = "fk_cartsdetail_product")]
public partial class Cartsdetail
{
    [Key]
    [Column("cart_detail_id")]
    public long CartDetailId { get; set; }

    [Column("cart_id")]
    public long? CartId { get; set; }

    [Column("product_id")]
    public long? ProductId { get; set; }

    [Column("quantity")]
    public int? Quantity { get; set; }

    [Column("total_price")]
    [Precision(10, 2)]
    public decimal? TotalPrice { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime UpdatedAt { get; set; }

    [Column("status", TypeName = "enum('waiting','canceled','fulfilled')")]
    public string? Status { get; set; }

    [ForeignKey("CartId")]
    [InverseProperty("Cartsdetails")]
    public virtual Cart? Cart { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Cartsdetails")]
    public virtual Product? Product { get; set; }
}
