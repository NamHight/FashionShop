using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;

namespace FashionShop_API.Services.Favorites
{
    public interface IServiceFavorites
    {
        Task<IEnumerable<ResponseFavoritesDto>?> GetListFavoritesByIdAsync(long? id, bool trackChanges);
    }
}
