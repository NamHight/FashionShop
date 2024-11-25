using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Models;

[Table("views")]
[Index("CustomerId", Name = "fk_views_customer")]
[Index("ProductId", Name = "fk_views_product")]
[Index("StoreId", Name = "fk_views_store")]
public partial class View
{
    [Key]
    [Column("view_id")]
    public long ViewId { get; set; }

    [Column("product_id")]
    public long? ProductId { get; set; }

    [Column("customer_id")]
    public long? CustomerId { get; set; }

    [Column("store_id")]
    public long? StoreId { get; set; }

    [Column("view_date", TypeName = "datetime")]
    public DateTime? ViewDate { get; set; }

    [Column("session_id")]
    [StringLength(50)]
    public string? SessionId { get; set; }

    [Column("status", TypeName = "enum('active','inactive')")]
    public string? Status { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Views")]
    public virtual Customer? Customer { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Views")]
    public virtual Product? Product { get; set; }

    [ForeignKey("StoreId")]
    [InverseProperty("Views")]
    public virtual Store? Store { get; set; }
}
