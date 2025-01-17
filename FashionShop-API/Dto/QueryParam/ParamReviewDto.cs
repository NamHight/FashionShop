namespace FashionShop_API.Dto.QueryParam
{
    public class ParamReviewDto
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
        public long ProductId { get; set; }
        public string TypeOrderBy { get; set; } = string.Empty;
        public int Rating { get; set; } = 0;
    }
}
