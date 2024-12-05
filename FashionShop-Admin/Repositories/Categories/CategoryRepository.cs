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
    public void AddNewCategory(Category category)
    {
        Create(category);
    }
    public async Task<bool> CheckSlug(string slug)
    {
        var result = await _context.Categories.Where(item => item.Slug.Equals(slug)).FirstOrDefaultAsync();
        if (result != null) return true;
        return false;
    }
    public async Task<bool> DeleteAsync(long id, bool trackChanges)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            //await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
    public void UpdateStatus(Category category)
    {
        Update(category);
    }
    public async Task<List<Category>> GetPageLinkAsync(int page, int pageSize, bool trackChanges)
    {
        var categories = await PageLinkAsync(page, pageSize, trackChanges);
        return categories;
    }
    public async Task<long> FindByNameAsync(string newCategoryName)
    {
        var category = await _context.Categories.Where(item => item.CategoryName == newCategoryName).FirstOrDefaultAsync();
        long id_category = category?.CategoryId ?? -1;
        return id_category;
    }
}