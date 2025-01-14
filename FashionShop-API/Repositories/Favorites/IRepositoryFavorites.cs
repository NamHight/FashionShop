using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;
namespace FashionShop_API.Repositories.Favorites
{
    public interface IRepositoryFavorites
    {
        Task<IEnumerable<Favorite>?> GetListFavoritesById(long id, bool trackChanges);
        Task AddAsync(Favorite entity, bool trackChanges);
        Task<IEnumerable<Favorite>> GetFavoriteByProductIdAsync(long productId);

        Task<Favorite> GetFavoriteByUserIdAndProductId(long userId, long productId);
        Task<bool> DeleteFavorite(Favorite favorite);

    }
}
