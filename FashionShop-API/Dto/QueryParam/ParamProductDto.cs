namespace FashionShop_API.Dto.QueryParam
{
    public abstract class ParamProductDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
