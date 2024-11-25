using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Models;

[Table("suppliers")]
public partial class Supplier
{
    [Key]
    [Column("supplier_id")]
    public long SupplierId { get; set; }

    [Column("supplier_name")]
    [StringLength(255)]
    public string SupplierName { get; set; } = null!;

    [Column("contact_name")]
    [StringLength(255)]
    public string ContactName { get; set; } = null!;

    [Column("phone")]
    [StringLength(15)]
    public string Phone { get; set; } = null!;

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("address", TypeName = "text")]
    public string? Address { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [Column("status", TypeName = "enum('active','inactive')")]
    public string? Status { get; set; }

    [InverseProperty("Supplier")]
    public virtual ICollection<Purchaseorder> Purchaseorders { get; set; } = new List<Purchaseorder>();
}
