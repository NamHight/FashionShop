using AutoMapper;
using FashionShop_API.Dto;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Exceptions;
using FashionShop_API.Repositories;
using FashionShop_API.Repositories.RepositoryManager;

namespace FashionShop_API.Services.Promotions
{
    public class ServicePromotion : IServicePromotion
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ServicePromotion(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<ResponsePromotionDto> data, PageInfo page)> GetAllPaginateAsync(int page, int limit)
        {
            var promotions = await _repositoryManager.Promotion.GetPaginateAsync(page, limit);
            var promotionDto = _mapper.Map<IEnumerable<ResponsePromotionDto>>(promotions);
            return (data: promotionDto, page: promotions.PageInfo);
        }

        public async Task<ResponsePromotionDto> GetByIdPromotionAsync(long id, bool trackChanges)
        {
            var promotion = await _repositoryManager.Promotion.GetByIdAsync(id, trackChanges);
            var promotionDto = _mapper.Map<ResponsePromotionDto>(promotion);
            return promotionDto;
        }

        public async Task<IEnumerable<ResponsePromotionDto>> GetPromotionAsync(bool trachChanges)
        {
            var promotions = await _repositoryManager.Promotion.GetAllPromotionAsync(trachChanges);
            var promotionDto = _mapper.Map<IEnumerable<ResponsePromotionDto>>(promotions);
            return promotionDto;
        }
    }
}
