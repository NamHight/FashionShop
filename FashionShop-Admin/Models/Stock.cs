using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Models;

[Table("stocks")]
[Index("ProductId", Name = "fk_inventory_product")]
[Index("StoreId", Name = "fk_inventory_store")]
public partial class Stock
{
    [Key]
    [Column("inventory_id")]
    public long InventoryId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("store_id")]
    public long? StoreId { get; set; }

    [Column("product_id")]
    public long? ProductId { get; set; }

    [Column("last_updated", TypeName = "timestamp")]
    public DateTime? LastUpdated { get; set; }

    [Column("status", TypeName = "enum('in_stock','out_of_stock')")]
    public string? Status { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Stocks")]
    public virtual Product? Product { get; set; }

    [ForeignKey("StoreId")]
    [InverseProperty("Stocks")]
    public virtual Store? Store { get; set; }
}
