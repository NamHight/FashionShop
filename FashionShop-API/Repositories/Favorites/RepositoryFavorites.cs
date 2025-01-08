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
                .Include(item => item.Product).Include(item => item.Product.Category).ToListAsync();
            return ListFavorites;
        }

		public async Task AddAsync(Favorite entity, bool trackChanges)
		{
			if (!trackChanges)
			{
				// Nếu không cần theo dõi thay đổi, tắt tracking
				_context.Entry(entity).State = EntityState.Added;
			}
			else
			{
				// Nếu cần theo dõi thay đổi, mặc định Entity Framework sẽ tự động track
				await _context.Set<Favorite>().AddAsync(entity);
			}

			// Lưu thay đổi vào cơ sở dữ liệu
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(long id)
		{
			var favorite = await _context.Set<Favorite>().FindAsync(id);

			if (favorite == null)
			{
				throw new KeyNotFoundException("Favorite not found.");
			}

			_context.Set<Favorite>().Remove(favorite);
			await _context.SaveChangesAsync(); // Lưu các thay đổi vào DB
		}
	}
}
