﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;

namespace FashionShop_API.Models;

[Table("customers")]
[Index("Email", Name = "email", IsUnique = true)]
public partial class Customer : IdentityUser
{
    [Key]
    [Column("customer_id")]
    public long CustomerId { get; set; }

    [Column("customer_name")]
    [StringLength(255)]
    public string CustomerName { get; set; } = null!;

    [Column("phone")]
    [StringLength(15)]
    public string? Phone { get; set; }

    [Column("password")]
    [StringLength(255)]
    public string Password { get; set; } = null!;

    [Column("email")]
    public string? Email { get; set; }

    [Column("avatar")]
    [StringLength(255)]
    public string? Avatar { get; set; }

    [Column("birth")]
    public DateOnly? Birth { get; set; }

    [Column("gender", TypeName = "enum('male','female')")]
    public string? Gender { get; set; }

    [Column("address", TypeName = "text")]
    public string? Address { get; set; }

    [Column("status", TypeName = "enum('active','warnning','banned')")]
    public string? Status { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("Customer")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("Customer")]
    public virtual ICollection<View> Views { get; set; } = new List<View>();
}
