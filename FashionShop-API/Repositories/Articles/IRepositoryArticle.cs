using FashionShop_API.Models;
using FashionShop_API.Repositories.Shared;

namespace FashionShop_API.Repositories.Articles
{
    public interface IRepositoryArticle
    {
        Task<PagedListAsync<Article>> GetPagingAndSearchAsync(int page, int limit, string searchName, int categoryid, bool trackChanges);
    }
}
