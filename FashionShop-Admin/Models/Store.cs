using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Models;

[Table("stores")]
[Index("StoreName", Name = "store_name", IsUnique = true)]
public partial class Store
{
    [Key]
    [Column("store_id")]
    public long StoreId { get; set; }

    [Column("store_name")]
    public string StoreName { get; set; } = null!;

    [Column("address", TypeName = "text")]
    public string? Address { get; set; }

    [Column("phone")]
    [StringLength(15)]
    public string Phone { get; set; } = null!;

    [Column("manager_id")]
    public long? ManagerId { get; set; }

    [Column("region")]
    [StringLength(255)]
    public string Region { get; set; } = null!;

    [Column("country")]
    [StringLength(255)]
    public string Country { get; set; } = null!;

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [Column("status", TypeName = "enum('active','inactive')")]
    public string? Status { get; set; }

    [InverseProperty("Store")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("Store")]
    public virtual ICollection<Purchaseorder> Purchaseorders { get; set; } = new List<Purchaseorder>();

    [InverseProperty("Store")]
    public virtual ICollection<View> Views { get; set; } = new List<View>();
}
