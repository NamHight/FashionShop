using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Models;

[Table("employees")]
[Index("Email", Name = "email", IsUnique = true)]
[Index("RoleId", Name = "fk_employees_role")]
[Index("StoreId", Name = "fk_employees_store")]
public partial class Employee
{
    [Key]
    [Column("employee_id")]
    public long EmployeeId { get; set; }

    [Column("employee_name")]
    [StringLength(255)]
    public string EmployeeName { get; set; } = null!;

    [Column("employee_position", TypeName = "enum('manager','sales','customer care','deliver','intern','stock')")]
    public string? EmployeePosition { get; set; }

    [Column("phone")]
    [StringLength(15)]
    public string? Phone { get; set; }

    [Column("avatar")]
    [StringLength(255)]
    public string? Avatar { get; set; }

    [Column("password")]
    [StringLength(255)]
    public string Password { get; set; } = null!;

    [Column("email")]
    public string Email { get; set; } = null!;

    [Column("store_id")]
    public long? StoreId { get; set; }

    [Column("role_id")]
    public int? RoleId { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [Column("status", TypeName = "enum('active','warnning','banned')")]
    public string? Status { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("RoleId")]
    [InverseProperty("Employees")]
    public virtual Role? Role { get; set; }

    [ForeignKey("StoreId")]
    [InverseProperty("Employees")]
    public virtual Store? Store { get; set; }
}
