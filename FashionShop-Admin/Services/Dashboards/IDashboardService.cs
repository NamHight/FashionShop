using FashionShop.Models.views.DashboardViewModel;

namespace FashionShop.Services.Dashboards;

public interface IDashboardService
{
    Task<List<SalesData>> GetListSalesDataByMonthInYear(int year, bool trackChanges);
    Task<List<StatusCustomer>> GetListStatusCustomer();
    
    Task<DataAllStatic> GetDataAllStatic(DateTime date, bool trackChanges);
}