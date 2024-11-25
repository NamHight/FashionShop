﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Models;

[Table("products")]
[Index("CategoryId", Name = "fk_products_category")]
public partial class Product
{
    [Key]
    [Column("product_id")]
    public long ProductId { get; set; }

    [Column("product_name")]
    [StringLength(255)]
    public string ProductName { get; set; } = null!;

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("price")]
    [Precision(10, 2)]
    public decimal? Price { get; set; }

    [Column("category_id")]
    public long? CategoryId { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [Column("status", TypeName = "enum('available','unavailable')")]
    public string? Status { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Cartsdetail> Cartsdetails { get; set; } = new List<Cartsdetail>();

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category? Category { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    [InverseProperty("Product")]
    public virtual ICollection<Ordersdetail> Ordersdetails { get; set; } = new List<Ordersdetail>();

    [InverseProperty("Product")]
    public virtual ICollection<Purchaseorderdetail> Purchaseorderdetails { get; set; } = new List<Purchaseorderdetail>();

    [InverseProperty("Product")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("Product")]
    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    [InverseProperty("Product")]
    public virtual ICollection<View> Views { get; set; } = new List<View>();
}