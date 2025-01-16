using FashionShop_API.Context;
using FashionShop_API.Models;
using FashionShop_API.Repositories.Views;
using Microsoft.EntityFrameworkCore;


namespace FashionShop_API.Repositories.Views
{
    public class RepositoryViews : RepositoryBase<View>, IRepositoryViews
    {
       
        public RepositoryViews(MyDbContext context) : base(context)
        {
        }

        // Kiểm tra nếu người dùng đã xem sản phẩm
        public async Task<bool> HasUserViewedProductAsync(long productId, string sessionId = null, long? customerId = null)
        {
            if (customerId.HasValue)
            {
                return await _context.Views
                    .AnyAsync(v => v.ProductId == productId && v.CustomerId == customerId.Value);
            }
            else if (!string.IsNullOrEmpty(sessionId))
            {
                return await _context.Views
                    .AnyAsync(v => v.ProductId == productId && v.SessionId == sessionId);
            }
            return false;
        }

        // Thêm lượt xem vào bảng Views
        public async Task AddViewAsync(long productId, string sessionId = null, long? customerId = null)
        {
            var view = new View
            {
                ProductId = productId,
                CustomerId = customerId,  // Nếu có customerId, lưu nó
                SessionId = sessionId,     // Nếu không có customerId, lưu sessionId hoặc thông tin khác
                ViewDate = DateTime.UtcNow
            };

            await _context.Views.AddAsync(view);
            await _context.SaveChangesAsync();
        }
    }

}
