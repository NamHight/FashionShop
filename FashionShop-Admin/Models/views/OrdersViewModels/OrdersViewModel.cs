using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FashionShop.Models.views.OrderDetails
{
    public class OrdersViewModel
    {
        public long OrderId { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Reciver { get; set; }
        public string? AddressCustomer { get; set; }
        public string? CustomerName { get; set; }
        public string? EmployeeName { get; set; }
        public string? OrdersStatus { get; set; }
        public string? PhoneCustomer { get; set; }
        public string? Phone { get; set; }
        public List<Ordersdetail>? Details { get; set; }
    }
}
