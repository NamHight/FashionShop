namespace FashionShop_API.Dto.QueryParam
{
    public class ParamArticleDto
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 9;
        public string nameSearch { get; set; } = string.Empty;
        public int Categoryid { get; set; } = 0;
    }
}
