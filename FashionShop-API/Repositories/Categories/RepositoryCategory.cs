using FashionShop_API.Context;
using FashionShop_API.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Repositories.Categories;

public class RepositoryCategory : RepositoryBase<Category> , IRepositoryCategory
{
    public RepositoryCategory(MyDbContext context) : base(context)
    {
    }


    public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges)
    {
        var categories = await FindAll(trackChanges).ToListAsync();
        return categories;
    }


    public async Task<Category?> GetCategoryByIdAsync(long id, bool trackChanges)
    {
        var category = await FindByCondition(c => c.CategoryId.Equals(id),trackChanges).FirstOrDefaultAsync();
        return category;
    }
}