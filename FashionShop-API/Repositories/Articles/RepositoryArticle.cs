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
                var articles = await FindByCondition(item => item.ArticleName.Contains(searchName) && item.Category_Id == categoryid && item.Status == 1,trackChanges).Include(item => item.Category).ToListAsync();
                return await PagedListAsync<Article>.ToPagedListAsync(articles.AsQueryable(), page, limit);
            }
            if (!string.IsNullOrEmpty(searchName))
            {
                var articles = await FindByCondition(item => item.ArticleName.Contains(searchName) && item.Status == 1, trackChanges).Include(item => item.Category).ToListAsync();
                return await PagedListAsync<Article>.ToPagedListAsync(articles.AsQueryable(), page, limit);
            }
            if (categoryid != 0)
            {
                var articles = await FindByCondition(item => item.Category_Id == categoryid && item.Status == 1, trackChanges).Include(item => item.Category).ToListAsync();
                return await PagedListAsync<Article>.ToPagedListAsync(articles.AsQueryable(), page, limit);
            }
            return await PagedListAsync<Article>.ToPagedListAsync(_context.Articles.Where(item => item.Status == 1).Include(item => item.Category).AsQueryable(), page, limit);
        }
    }
}
