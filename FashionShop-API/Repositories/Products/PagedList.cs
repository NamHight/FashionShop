using FashionShop_API.Dto;

namespace FashionShop_API.Repositories.Products
{
    public class PagedList<T> : List<T>
    {
        public PageInfo PageInfo { get; set; }
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            PageInfo = new PageInfo
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange(items);
        }
        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int
        pageSize)
        {
            var count = source.Count();
            var items = source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
