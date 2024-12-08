using FashionShop.Context;
using FashionShop.Models;
using FashionShop.Models.views.OrderDetails;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Repositories.Orders;

public class OrdersRepository : GenericRepo<Order>,IOrdersRepository
{
    public OrdersRepository(MyDbContext context) : base(context)
    {
    }
    public async Task<Order?> GetByIdOrdersViewModel(long id, bool trackChanges)
    {
        var order = await FindById(item => item.OrderId == id, trackChanges).Include(item => item.Customer).Include(item => item.Store).Include(item => item.Employee).Include(item => item.Ordersdetails).ThenInclude(item => item.Product).FirstOrDefaultAsync();
        return order;
    }
    public async Task<List<Order>> GetAllAsync(bool trackChanges)
    {
        var orders = await FindAll(trackChanges).Include(p => p.Store).Include(p=> p.Employee).
        Include(p=>p.Customer).ToListAsync();
        return orders;
    }
    public async Task<Order?> GetByIdOrders(long id, bool trackChanges)
    {
        var orders = await FindById(item => item.OrderId == id, trackChanges).Include(p => p.Store).Include(p => p.Employee).
        Include(p => p.Customer).FirstOrDefaultAsync();

        return orders;
    }
    public async Task<Order?> GetByIdAsync(long id, bool trackChanges)
    {
        var orders = await FindById(item => item.OrderId == id, trackChanges).FirstOrDefaultAsync();
       
        return orders;
    }
    public async Task Remove(Order order)
    {
        try
        {
           Delete(order);
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