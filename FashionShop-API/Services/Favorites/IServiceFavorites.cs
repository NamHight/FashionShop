using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;
using NuGet.Protocol.Core.Types;

namespace FashionShop_API.Services.Favorites
{
    public interface IServiceFavorites
    {
        Task<IEnumerable<ResponseFavoritesDto>?> GetListFavoritesByIdAsync(long? id, bool trackChanges);
		Task AddFavoriteAsync(RequestFarvoriteDto farvorite, bool trackChanges);
        Task<bool> DeleteFavoriteAsync(long userId, long productId);
    }
}
