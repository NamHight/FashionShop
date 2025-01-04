using FashionShop_API.Context;
using FashionShop_API.Dto.ResponseDto;
using FashionShop_API.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Repositories.Favorites
{
    public class RepositoryFavorites : RepositoryBase<Favorite>, IRepositoryFavorites
    {
        public RepositoryFavorites(MyDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Favorite>?> GetListFavoritesById(long id, bool trackChanges)
        {
            var ListFavorites = await FindByCondition(item => item.CustomerId.Equals(id), trackChanges)
                .Include(item => item.Product).Include(item => item.Product.Category)
                .Include(item => item.Product.Store).ToListAsync();
            return ListFavorites;
        }



    }
}
