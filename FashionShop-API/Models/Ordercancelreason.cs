using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Models;

[Table("ordercancelreason")]
[Index("OrderId", Name = "order_id")]
public partial class Ordercancelreason
{
    [Key]
    [Column("cancelorder_id")]
    public int CancelorderId { get; set; }

    [Column("reason", TypeName = "text")]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? Reason { get; set; }

    [Column("order_id")]
    public long? OrderId { get; set; }

    [Column("create_at", TypeName = "timestamp")]
    public DateTime? CreateAt { get; set; }

    [Column("status", TypeName = "enum('pending','approved','rejected')")]
    public string? Status { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Ordercancelreasons")]
    public virtual Order? Order { get; set; }
}
