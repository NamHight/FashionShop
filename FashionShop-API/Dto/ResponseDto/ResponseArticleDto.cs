using FashionShop_API.Models;

namespace FashionShop_API.Dto.ResponseDto
{
    public class ResponseArticleDto
    {
        public long ArticleId { get; set; }
        public string? ArticlesName { get; set; }
        public string? slug { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public long CategoryId { get; set; }
        public DateTime CreateAt { get; set; }
        public ResponseCategoryDto Category { get; set; }
    }
    
}
