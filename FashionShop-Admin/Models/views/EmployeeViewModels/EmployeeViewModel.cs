namespace FashionShop.Models.views.EmployeeViewModel;

public class EmployeeViewModel
{
    public IEnumerable<Employee> Employees { get; set; }
    public PagingInfo PagingInfo { get; set; }
}