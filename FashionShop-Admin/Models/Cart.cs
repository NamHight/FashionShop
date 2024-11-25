using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Models;

[Table("carts")]
[Index("CustomerId", Name = "fk_carts_customer")]
public partial class Cart
{
    [Key]
    [Column("cart_id")]
    public long CartId { get; set; }

    [Column("customer_id")]
    public long? CustomerId { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime UpdatedAt { get; set; }

    [Column("status", TypeName = "enum('waiting','canceled','completed')")]
    public string? Status { get; set; }

    [InverseProperty("Cart")]
    public virtual ICollection<Cartsdetail> Cartsdetails { get; set; } = new List<Cartsdetail>();

    [ForeignKey("CustomerId")]
    [InverseProperty("Carts")]
    public virtual Customer? Customer { get; set; }
}
