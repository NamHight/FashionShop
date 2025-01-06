using FashionShop_API.Dto;
using FashionShop_API.Dto.QueryParam;
using FashionShop_API.Dto.ResponseDto;

namespace FashionShop_API.Services.Articles
{
    public interface IServiceArticle
    {
        Task<(IEnumerable<ResponseArticleDto> data, PageInfo page)> GetPagingAndSearchAsync(ParamArticleDto paramArticleDto);
    }
}
