namespace FashionShop_API.Dto.QueryParam
{
    public abstract class ParamProductDto
    {
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;

    }
}
