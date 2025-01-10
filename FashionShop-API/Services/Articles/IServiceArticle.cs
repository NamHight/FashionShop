using FashionShop_API.Dto;
using FashionShop_API.Dto.QueryParam;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;
using FashionShop_API.Repositories.Shared;

namespace FashionShop_API.Services.Articles
{
    public interface IServiceArticle
    {
        Task<(IEnumerable<ResponseArticleDto> data, PageInfo page)> GetPagingAndSearchAsync(ParamArticleDto paramArticleDto);
    }
}
