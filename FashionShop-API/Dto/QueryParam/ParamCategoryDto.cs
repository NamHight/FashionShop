namespace FashionShop_API.Dto.QueryParam;

public class ParamCategoryDto
{
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; } = "asc";
}