using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Models;

[Table("purchaseorders")]
[Index("StoreId", Name = "fk_purchaseorders_store")]
[Index("SupplierId", Name = "fk_purchaseorders_supplier")]
public partial class Purchaseorder
{
    [Key]
    [Column("purchase_order_id")]
    public long PurchaseOrderId { get; set; }

    [Column("total_amount")]
    [Precision(10, 2)]
    public decimal? TotalAmount { get; set; }

    [Column("order_date")]
    public DateOnly? OrderDate { get; set; }

    [Column("store_id")]
    public long? StoreId { get; set; }

    [Column("supplier_id")]
    public long? SupplierId { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [Column("status", TypeName = "enum('pending','completed','canceled')")]
    public string? Status { get; set; }

    [InverseProperty("PurchaseOrder")]
    public virtual ICollection<Purchaseorderdetail> Purchaseorderdetails { get; set; } = new List<Purchaseorderdetail>();

    [ForeignKey("StoreId")]
    [InverseProperty("Purchaseorders")]
    public virtual Store? Store { get; set; }

    [ForeignKey("SupplierId")]
    [InverseProperty("Purchaseorders")]
    public virtual Supplier? Supplier { get; set; }
}
