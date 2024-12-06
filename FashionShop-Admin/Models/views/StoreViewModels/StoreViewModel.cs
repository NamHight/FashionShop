namespace FashionShop.Models.views.StoreViewModels;

public class StoreViewModel
{
    public IEnumerable<Store> Stores { get; set; }
    
    public PagingInfo PagingInfo { get; set; }
}