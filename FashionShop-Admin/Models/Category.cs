using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Models;

[Table("categories")]
[Index("CategoryName", Name = "category_name")]
[Index("ParentId", Name = "fk_category_id")]
[Index("Slug", Name = "slug")]
public partial class Category
{
    [Key]
    [Column("category_id")]
    public long CategoryId { get; set; }

    [Column("category_name")]
    public string CategoryName { get; set; } = null!;

    [Column("slug")]
    public string Slug { get; set; } = null!;

    [Column("parentId")]
    public long? ParentId { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [Column("status", TypeName = "enum('active','inactive')")]
    public string? Status { get; set; }

    [InverseProperty("Parent")]
    public virtual ICollection<Category> InverseParent { get; set; } = new List<Category>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual Category? Parent { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
