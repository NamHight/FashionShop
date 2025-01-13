using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Models;

[Table("orders")]
[Index("CustomerId", Name = "fk_orders_customer")]
[Index("EmployeeId", Name = "fk_orders_employee")]
public partial class Order
{
    [Key]
    [Column("order_id")]
    public long OrderId { get; set; }

    [Column("total_amount")]
    [Precision(10, 2)]
    public decimal? TotalAmount { get; set; }

    [Column("order_date", TypeName = "datetime")]
    public DateTime? OrderDate { get; set; }

    [Column("payment_method", TypeName = "enum('cash','credit_card','debit_card','paypal')")]
    public string? PaymentMethod { get; set; }

    [Column("employee_id")]
    public long? EmployeeId { get; set; }

    [Column("customer_id")]
    public long CustomerId { get; set; }

    [Column("reciver")]
    [StringLength(200)]
    public string? Reciver { get; set; }

    [Column("address")]
    [StringLength(200)]
    public string? Address { get; set; }

    [Column("phone")]
    [StringLength(15)]
    public string? Phone { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [Column("status", TypeName = "enum('processing','delivering','completed','canceled')")]
    public string? Status { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("EmployeeId")]
    [InverseProperty("Orders")]
    public virtual Employee? Employee { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<Ordersdetail> Ordersdetails { get; set; } = new List<Ordersdetail>();
}
