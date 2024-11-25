using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Models;

[Table("stocks")]
[Index("ProductId", Name = "fk_inventory_product")]
[Index("StockName", Name = "stock_name", IsUnique = true)]
public partial class Stock
{
    [Key]
    [Column("stock_id")]
    public long StockId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("product_id")]
    public long? ProductId { get; set; }

    [Column("stock_name")]
    public string? StockName { get; set; }

    [Column("address")]
    [StringLength(255)]
    public string? Address { get; set; }

    [Column("expiration_date", TypeName = "datetime")]
    public DateTime? ExpirationDate { get; set; }

    [Column("last_updated", TypeName = "timestamp")]
    public DateTime? LastUpdated { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [Column("status", TypeName = "enum('in_stock','out_of_stock')")]
    public string? Status { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Stocks")]
    public virtual Product? Product { get; set; }
}
