using AutoMapper;
using FashionShop_API.Dto;
using FashionShop_API.Dto.QueryParam;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;
using FashionShop_API.Repositories;
using FashionShop_API.Repositories.Shared;

namespace FashionShop_API.Services.Articles
{
    public class ServiceArticle : IServiceArticle
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ServiceArticle(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<ResponseArticleDto> data, PageInfo page)> GetPagingAndSearchAsync(ParamArticleDto paramArticleDto)
        {
            var articles = await _repositoryManager.Article.GetPagingAndSearchAsync(paramArticleDto.Page, paramArticleDto.Limit, paramArticleDto.nameSearch, paramArticleDto.Categoryid, trackChanges: false);
            var articlesDto = _mapper.Map<IEnumerable<ResponseArticleDto>>(articles);
            return (data: articlesDto, page: articles.PageInfo);
        }
    }
}
