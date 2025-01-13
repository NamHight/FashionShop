using FashionShop.Context;
using FashionShop.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

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
        // Tìm danh mục theo ID
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return false;  // Nếu không tìm thấy danh mục
        }

        // Kiểm tra nếu có sản phẩm nào đang sử dụng danh mục này
        var productWithCategory = await _context.Products
            .Where(p => p.CategoryId == id)
            .FirstOrDefaultAsync();

        if (productWithCategory != null)
        {
            // Nếu có sản phẩm liên quan, không cho phép xóa
            return false;
        }

        // Nếu không có sản phẩm liên quan, tiến hành xóa danh mục
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return true;
    }
    public void UpdateStatus(Category category)
    {
        Update(category);
    }
    public async Task<List<Category>> GetPageLinkAsync(int page, int pageSize, string nameSearch, string typeCategory, bool trackChanges)
    {
        if (!string.IsNullOrEmpty(nameSearch))
        {
            nameSearch = nameSearch.ToLower().Trim();
           
            if (typeCategory == "children")
            {
                return await _context.Categories.Where(item => item.CategoryName.ToLower().Contains(nameSearch) && item.ParentId != null).Skip((page - 1) * pageSize).Take(pageSize).OrderByDescending(item => item.CreatedAt).ToListAsync();
            }
            return await _context.Categories.Where(item => item.CategoryName.ToLower().Contains(nameSearch) && item.ParentId == null).Skip((page - 1) * pageSize).Take(pageSize).OrderByDescending(item => item.CreatedAt).ToListAsync();
        }
        return await PageLinkAsync(page, pageSize, trackChanges).OrderByDescending(item => item.CreatedAt).ToListAsync();
    }

    public async Task<int> GetCountAsync(string nameSearch, string typeCategory, bool trackChanges)
    {
        if (!string.IsNullOrEmpty(nameSearch))
        {
            nameSearch = nameSearch.ToLower().Trim();
            if (typeCategory == "children")
            {
                return await _context.Categories.Where(item => item.CategoryName.ToLower().Contains(nameSearch) && item.ParentId != null).CountAsync();
            }
            return await _context.Categories.Where(item => item.CategoryName.ToLower().Contains(nameSearch) && item.ParentId == null).CountAsync();

        }
        return await FindAll(trackChanges).CountAsync();
    }

    public async Task<long> FindByNameAsync(string newCategoryName)
    {
        var category = await _context.Categories.Where(item => item.CategoryName == newCategoryName).FirstOrDefaultAsync();
        long id_category = category?.CategoryId ?? -1;
        return id_category;
    }
    public async Task<int> CountCategoryAsync()
    {
        return await _context.Categories.CountAsync();
    }

}