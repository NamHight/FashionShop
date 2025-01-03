using AutoMapper;
using FashionShop_API.Dto;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Exceptions;
using FashionShop_API.Models;
using FashionShop_API.Repositories.RepositoryManager;
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
                //var listFavoritesDto = _mapper.Map<IEnumerable<ResponseFavoritesDto>>(listFavorites);
                var listFavoritesDto = new List<ResponseFavoritesDto>();
                foreach (var item in listFavorites)
                {
                    var favoritesDto = new ResponseFavoritesDto
                    {
                        FavoriteId = item.FavoriteId,
                        ProductName = item.Product.ProductName,
                        Banner = item.Product.Banner,
                        Price = item.Product.Price,
                        CategoryName = item.Product.Category.CategoryName
                    };
                    listFavoritesDto.Add(favoritesDto);
                }
                return listFavoritesDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: ", ex.Message);
                throw;
            }
        }
    }
}
