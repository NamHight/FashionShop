using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Models;

[Table("categories")]
[Index("CategoryName", Name = "category_name", IsUnique = true)]
[Index("Slug", Name = "slug", IsUnique = true)]
public partial class Category
{
    [Key]
    [Column("category_id")]
    public long CategoryId { get; set; }

    [Column("category_name")]
    public string CategoryName { get; set; } = null!;

    [Column("slug")]
    public string Slug { get; set; } = null!;

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [Column("status", TypeName = "enum('active','inactive')")]
    public string? Status { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
