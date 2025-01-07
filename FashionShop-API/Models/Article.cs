using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Models;

[Table("articles")]
[Index("CategoryId", Name = "fk_category_articles")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class Article
{
    [Key]
    [Column("article_id")]
    public ulong ArticleId { get; set; }

    [Column("articles_name")]
    [StringLength(255)]
    public string ArticlesName { get; set; } = null!;

    [Column("slug")]
    [StringLength(255)]
    public string? Slug { get; set; }

    [Column("category_id")]
    public long? CategoryId { get; set; }

    [Column("image")]
    [StringLength(255)]
    public string? Image { get; set; }

    [Column("description")]
    [StringLength(255)]
    public string? Description { get; set; }

    [Column("create_at", TypeName = "timestamp")]
    public DateTime? CreateAt { get; set; }

    [Column("status")]
    public int? Status { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Articles")]
    public virtual Category? Category { get; set; }
}
