using FashionShop_API.Models;
using FashionShop_API.Repositories.Shared;

namespace FashionShop_API.Repositories.Promotions
{
    public interface IRepositoryPromotion
    {
        Task<IEnumerable<Promotion>> GetAllPromotionAsync(bool trackChanges);
        Task<PagedListAsync<Promotion>> GetPaginateAsync(int page, int limit);
    }
}
