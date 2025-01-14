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
				_context.Entry(entity).State = EntityState.Added;
			}
			else
			{
				await _context.Set<Favorite>().AddAsync(entity);
			}

			// Lưu thay đổi vào cơ sở dữ liệu
			await _context.SaveChangesAsync();
		}
        public async Task<Favorite> GetFavoriteByUserIdAndProductId(long userId, long productId)
        {
            return await _context.Favorites
                .FirstOrDefaultAsync(f => f.CustomerId == userId && f.ProductId == productId);
        }

        // Xóa yêu thích khỏi cơ sở dữ liệu
        public async Task<bool> DeleteFavorite(Favorite favorite)
        {
            _context.Favorites.Remove(favorite);
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public async Task<IEnumerable<Favorite>> GetFavoriteByProductIdAsync(long productId)
        {
            return await _context.Favorites
                .Where(f => f.ProductId == productId) // Tìm các bản ghi có productId tương ứng
                .ToListAsync(); // Chuyển đổi kết quả thành một danh sách
        }
    }
}
