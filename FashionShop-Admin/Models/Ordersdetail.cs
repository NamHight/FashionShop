using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Models;

[Table("ordersdetails")]
[Index("OrderId", Name = "fk_ordersdetails_order")]
[Index("ProductId", Name = "fk_ordersdetails_product")]
public partial class Ordersdetail
{
    [Key]
    [Column("order_detail_id")]
    public long OrderDetailId { get; set; }

    [Column("quantity")]
    public int? Quantity { get; set; }
  
    [Column("total_price")]
    [Precision(10, 2)]
    public decimal? TotalPrice { get; set; }

    [Column("order_id")]
    public long? OrderId { get; set; }

    [Column("product_id")]
    public long? ProductId { get; set; }

    [Column("status", TypeName = "enum('pending','fulfilled')")]
    public string? Status { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Ordersdetails")]
    public virtual Order? Order { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Ordersdetails")]
    public virtual Product? Product { get; set; }
}
