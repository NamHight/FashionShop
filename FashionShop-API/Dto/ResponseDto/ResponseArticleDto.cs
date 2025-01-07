using FashionShop_API.Models;

namespace FashionShop_API.Dto.ResponseDto
{
    public class ResponseArticleDto
    {
        public long ArticleId { get; set; }
        public string? ArticlesName { get; set; }
        public string? slug { get; set; }
        public string? Description { get; set; }
        public long Category_Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public ResponseCategoryDto Category { get; set; }
    }
    
}
