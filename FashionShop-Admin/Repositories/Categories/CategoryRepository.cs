using FashionShop.Context;
using FashionShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Repositories.Categories;

public class CategoryRepository : GenericRepo<Category>,ICategoryRepository
{
    public CategoryRepository(MyDbContext context) : base(context)
    {
    }

    public async Task<List<Category>> GetAllAsync(bool trackChanges)
    {
        var categories = await FindAll(trackChanges).ToListAsync();
        return categories;
    }

    public async Task<Category?> GetByIdAsync(long id, bool trackChanges)
    {
        var category = await FindById(item => item.CategoryId == id, trackChanges).FirstOrDefaultAsync();
        return category;
    }

    public void CreateCategoryAsync(Category category)
    {
        Create(category);
    }
    
}