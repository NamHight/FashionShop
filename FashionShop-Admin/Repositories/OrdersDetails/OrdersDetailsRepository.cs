using FashionShop.Context;
using FashionShop.Models;
using FashionShop.Models.views.OrderDetails;
using Microsoft.EntityFrameworkCore;
using FashionShop.Repositories.Orders;

namespace FashionShop.Repositories.OrdersDetails;

public class OrdersDetailsRepository : GenericRepo<Ordersdetail>,IOrdersDetailsRepository
{
    public OrdersDetailsRepository(MyDbContext context) : base(context)
    {
    }

    public async Task RemoveDetails(long id,bool trackChanges)
    {
        try
        {
            var orderdetails = await FindById(item => item.OrderId.Equals(id), trackChanges).ToListAsync();
            _context.Ordersdetails.RemoveRange(orderdetails);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            Console.WriteLine("Lỗi cạnh tranh dữ liệu: " + ex.Message);
            throw;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine("Lỗi cập nhật dữ liệu: " + ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi không xác định: " + ex.Message);
            throw;
        }

    }


}