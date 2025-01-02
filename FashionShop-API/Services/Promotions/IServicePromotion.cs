using FashionShop_API.Dto;
using FashionShop_API.Dto.ResponseDto;

namespace FashionShop_API.Services.Promotions
{
    public interface IServicePromotion
    {
        Task<IEnumerable<ResponsePromotionDto>> GetPromotionAsync(bool trackChanges);
        Task<(IEnumerable<ResponsePromotionDto> data, PageInfo page)> GetAllPaginateAsync(int page, int limit);
    }
}
