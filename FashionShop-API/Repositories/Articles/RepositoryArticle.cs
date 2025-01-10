using FashionShop_API.Context;
using FashionShop_API.Models;
using FashionShop_API.Repositories.Shared;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Repositories.Articles
{
    public class RepositoryArticle : RepositoryBase<Article>, IRepositoryArticle
    {
        public RepositoryArticle(MyDbContext context) : base(context) { }
        public async Task<PagedListAsync<Article>> GetPagingAndSearchAsync(int page, int limit, string searchName, int categoryid, bool trackChanges)
        {
            if(!string.IsNullOrEmpty(searchName) && categoryid != 0)
            {
                var articles = FindByCondition(item => item.ArticlesName.Contains(searchName) && item.CategoryId == categoryid && item.Status == 1, trackChanges).Include(item => item.Category);
                return await PagedListAsync<Article>.ToPagedListAsync(articles, page, limit);
            }
            if (!string.IsNullOrEmpty(searchName))
            {
                var articles = FindByCondition(item => item.ArticlesName.Contains(searchName) && item.Status == 1, trackChanges).Include(item => item.Category);
                return await PagedListAsync<Article>.ToPagedListAsync(articles, page, limit);
            }
            if (categoryid != 0)
            {
                var articles = FindByCondition(item => item.CategoryId == categoryid && item.Status == 1, trackChanges).Include(item => item.Category);
                return await PagedListAsync<Article>.ToPagedListAsync(articles, page, limit);
            }
            return await PagedListAsync<Article>.ToPagedListAsync(_context.Articles.Where(item => item.Status == 1).Include(item => item.Category).AsQueryable(), page, limit);
        }
    }
}
