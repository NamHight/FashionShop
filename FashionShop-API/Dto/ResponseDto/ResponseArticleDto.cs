using FashionShop_API.Models;

namespace FashionShop_API.Dto.ResponseDto
{
    public record ResponseArticleDto(long ArticleId, string ArticleName, string slug, string Image, string Description, long Category_Id, DateTime CreatedAt);
    
}
