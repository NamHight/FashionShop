using FashionShop.Models.views.DashboardViewModel;
using FashionShop.Repositories.ManagerRepository;

namespace FashionShop.Services.Dashboards;

public class DashboardService : IDashboardService
{
    private readonly IManagerRepository _managerRepository;

    public DashboardService(IManagerRepository managerRepository)
    {
        _managerRepository = managerRepository;
    }

    public async Task<List<SalesData>> GetListSalesDataByMonthInYear(int year, bool trackChanges)
    {
        try
        {
            var months = new[]
            {
                "January", "February", "March", "April", "May", "June", "July", "August", "September", "October",
                "November", "December"
            };
            var listPurchase = new List<SalesData>();
            foreach (var month in months)
            {
                var count = await _managerRepository.Orders.CountByMonthInYearAsync(year,
                    months.ToList().IndexOf(month) + 1, trackChanges);
                listPurchase.Add(new SalesData(month, count));
            }

            return listPurchase;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<List<RevenuaData>> GetListRevenuaDataByMonthInYear(int year, bool trackChanges)
    {
        try
        {
            var months = new[]
            {
                "January", "February", "March", "April", "May", "June", "July", "August", "September", "October",
                "November", "December"
            };
            var listRevenue = new List<RevenuaData>();
            foreach (var month in months)
            {
                var sum = await _managerRepository.Orders.SumByMonthInYearAsync(year,
                    months.ToList().IndexOf(month) + 1, trackChanges);
                    listRevenue.Add(new RevenuaData(month, sum));
            }

            return listRevenue;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<List<StatusCustomer>> GetListStatusCustomer()
    {
        try
        {
            var status = new[]
            {
                "active", "warnning", "banned"
            };
            var listStatus = new List<StatusCustomer>();
            foreach (var item in status)
            {
                var count = await _managerRepository.Customer.CountStatusAsync(item);
                listStatus.Add(new StatusCustomer(item, count));
            }

            return listStatus;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

    public async Task<DataAllStatic> GetDataAllStatic(DateTime date, bool trackChanges)
    {
        try
        {
            var customer = await _managerRepository.Customer.CountCustomerByMonthInYearAsync(date);
            var order = await _managerRepository.Orders.CountByDateAsync(date, false);
            var product = await _managerRepository.Product.CountByDateAsync(date, false);
            var totalSale = await _managerRepository.Orders.TotalSaleAsync(date, false);
            var avgSale = await _managerRepository.Orders.AvgSaleAsync(date, false);
            var result = new DataAllStatic(customer, order, totalSale, product, avgSale);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}