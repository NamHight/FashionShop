namespace FashionShop_API.Dto;

public class PageInfo
{
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public bool HasNextpage => CurrentPage < TotalPages;
    public bool HasPreviouspage => CurrentPage > 1;
}