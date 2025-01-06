using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;
namespace FashionShop_API.Repositories.Favorites
{
    public interface IRepositoryFavorites
    {
        Task<IEnumerable<Favorite>?> GetListFavoritesById(long id, bool trackChanges);
        Task AddAsync(Favorite entity, bool trackChanges);
        Task DeleteAsync(long id);

	}
}
