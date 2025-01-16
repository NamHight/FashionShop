using AutoMapper;
using FashionShop_API.Dto;
using FashionShop_API.Dto.RequestDto;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Exceptions;
using FashionShop_API.Models;
using FashionShop_API.Repositories;
using FashionShop_API.Services;
using FashionShop_API.Services.Favorites;

namespace FashionShop_API.Services.Favorites
{
    public class ServiceFavorites : IServiceFavorites
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public ServiceFavorites(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<ResponseFavoritesDto>?> GetListFavoritesByIdAsync(long? id, bool trackChanges)
        {
            try
            {
                var listFavorites = await _repositoryManager.Favorite.GetListFavoritesById(id.Value, trackChanges);
                if (listFavorites is null)
                {
                    throw new ListFavoriteNotFoundException("");
                }
                var listFavoritesDto = _mapper.Map<IEnumerable<ResponseFavoritesDto>>(listFavorites);
                return listFavoritesDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: ", ex.Message);
                throw;
            }
        }
		public async Task AddFavoriteAsync(RequestFarvoriteDto farvorite, bool trackChanges)
		{
			var farvoriteEntity = new Favorite
			{
				ProductId = farvorite.ProductId,
				CustomerId = farvorite.CustomerId,
                Status="active"
				
			};
			await _repositoryManager.Favorite.AddAsync(farvoriteEntity, trackChanges);
		}
        public async Task<bool> DeleteFavoriteAsync(long userId, long productId)
        {
            
                // Lấy yêu thích của userId và productId
                var favorite = await _repositoryManager.Favorite.GetFavoriteByUserIdAndProductId(userId, productId);

                if (favorite == null)
                {
                    return false;
                }

                // Xóa yêu thích
                return await _repositoryManager.Favorite.DeleteFavorite(favorite);
            
           
        }

    }
}
