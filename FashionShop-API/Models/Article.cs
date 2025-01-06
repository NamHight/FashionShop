using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FashionShop_API.Models
{
    [Table("articles")]
    public partial class Article
    {
        [Key]
        [Column("article_id")]
        public long ArticleId { get; set; }
        [Column("articles_name")]
        public string ArticleName { get; set; }
        [Column("slug")]
        public string? slug { get; set; }
        [Column("image")]
        public string? Image { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("category_id")]
        public long Category_Id { get; set; }
        [Column("create_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("status")]
        public int Status { get; set; } = 1;
        [ForeignKey("Category_Id")]
        public virtual Category? Category { get; set; }
    }
}
