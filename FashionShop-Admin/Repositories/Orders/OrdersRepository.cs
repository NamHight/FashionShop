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
        var order = await FindById(item => item.OrderId == id, trackChanges)
            .Include(item => item.Customer).Include(item => item.Employee)
            .Include(item => item.Ordersdetails).ThenInclude(item => item.Product).ThenInclude(item => item.Category).FirstOrDefaultAsync();
        return order;
    }
    public async Task<List<Order>> GetAllAsync(bool trackChanges)
    {
        var orders = await FindAll(trackChanges).Include(p=> p.Employee).
        Include(p=>p.Customer).ToListAsync();
        return orders;
    }
    public async Task<Order?> GetByIdOrders(long id, bool trackChanges)
    {
        var orders = await FindById(item => item.OrderId == id, trackChanges).Include(p => p.Employee).
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

    public async Task<int> CountByMonthInYearAsync(int year,int month ,bool trackChanges)
    {
        var count = await FindById(order => order.CreatedAt != null && order.CreatedAt.Value.Year.Equals(year) && order.CreatedAt.Value.Month.Equals(month), trackChanges).CountAsync();
        return count;
    }

    public async Task<decimal?> SumByMonthInYearAsync(int year, int month, bool trackChanges)
    {
        var query = FindById(order => order.CreatedAt != null && order.CreatedAt.Value.Year.Equals(year) && order.CreatedAt.Value.Month.Equals(month), trackChanges);
        var sum = await query.Select(order => order.TotalAmount).SumAsync();
        return sum;
    }

    public async Task<int> CountByDateAsync(DateTime date, bool trackChanges)
    {
       var count = await FindById(order => order.CreatedAt != null && order.CreatedAt.Value.Date.Equals(date.Date), trackChanges).CountAsync();
       return count;
    }

    public async Task<decimal?> TotalSaleAsync(DateTime date,bool trackChanges)
    {
        var total = await FindById(order => order.CreatedAt != null && order.CreatedAt.Value.Date.Equals(date.Date),trackChanges)
            .SumAsync(item => item.TotalAmount);
        return total;
    }

    public async Task<decimal?> AvgSaleAsync(DateTime date, bool trackChanges)
    {
        var avg = await FindById(order => order.CreatedAt.Value.Date.Equals(date.Date), trackChanges)
            .AverageAsync(order => order.TotalAmount);
        return avg;
    }

	public async Task<int?> countProductPerMonth(int month, int year)
    {
        var result =  _context.Orders.Where(p => p.OrderDate.Value.Month.Equals(month) && p.OrderDate.Value.Year.Equals(year)).SelectMany(p => p.Ordersdetails).Sum(x => x.Quantity);
        return result ?? 0;
    }
    public async Task<int> CountOrderAsync()
    {
        return await _context.Orders.CountAsync();
    }
}