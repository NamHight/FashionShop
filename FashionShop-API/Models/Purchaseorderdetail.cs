using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Models;

[Table("purchaseorderdetails")]
[Index("ProductId", Name = "fk_purchaseorderdetails_product")]
[Index("PurchaseOrderId", Name = "fk_purchaseorderdetails_purchaseorder")]
public partial class Purchaseorderdetail
{
    [Key]
    [Column("purchase_order_detail_id")]
    public long PurchaseOrderDetailId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("unit_price")]
    [Precision(10, 2)]
    public decimal? UnitPrice { get; set; }

    [Column("total_price")]
    [Precision(10, 2)]
    public decimal? TotalPrice { get; set; }

    [Column("purchase_order_id")]
    public long? PurchaseOrderId { get; set; }

    [Column("product_id")]
    public long? ProductId { get; set; }

    [Column("status", TypeName = "enum('pending','processed')")]
    public string? Status { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Purchaseorderdetails")]
    public virtual Product? Product { get; set; }

    [ForeignKey("PurchaseOrderId")]
    [InverseProperty("Purchaseorderdetails")]
    public virtual Purchaseorder? PurchaseOrder { get; set; }
}
