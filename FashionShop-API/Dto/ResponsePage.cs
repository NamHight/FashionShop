namespace FashionShop_API.Dto;

public class ResponsePage<T>
{
    public List<T> Data { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }
    public class Builder
    {
        private List<T> _data;
        private int _currentPage;
        private int _totalPages;
        private int _pageSize;
        private bool _hasNext;
        private bool _hasPrevious;
        private Builder()
        {
            
        }

        public static Builder Empty() => new();

        public Builder SetCurrentPage(int page)
        {
            _currentPage = page;
            return this;
        }

        public Builder SetTotalPages(int totalPages)
        {
            _totalPages = totalPages;
            return this;
        }

        public Builder SetPageSize(int pageSize)
        {
            _pageSize = pageSize;
            return this;
        }

        public Builder SetHasNext(bool hasNext)
        {
            _hasNext = hasNext;
            return this;
        }

        public Builder SetHasPrevious(bool hasPrevious)
        {
            _hasPrevious = hasPrevious;
            return this;
            
        }
        public Builder SetData(List<T> data)
        {
            _data = data;
            return this;
        }

        public ResponsePage<T> Build() => new ResponsePage<T>
        {
            TotalPages = _totalPages,
            CurrentPage = _currentPage,
            PageSize = _pageSize,
            HasNext = _hasNext,
            HasPrevious = _hasPrevious,
            Data = _data
        };

    }
}