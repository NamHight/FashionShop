namespace FashionShop.Models.views.WebsiteViewModel;

public class WebsiteViewModel
{
    public IEnumerable<WebsiteInfo> WebsiteInfos { get; set; }

    public PagingInfo PagingInfo { get; set; }
}