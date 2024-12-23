using System.Linq.Expressions;
using FashionShop_API.Models;

namespace FashionShop_API.Repositories.Categories;

public static class CategoryExtension
{
    public static IQueryable<Category> SearchByName(this IQueryable<Category> categories,string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return categories;
        }
        
        return categories.Where(category => category.CategoryName.Contains(searchTerm));
    }

    public static IQueryable<Category> SortById(this IQueryable<Category> categories, string? sortOrder)
    {
        if (string.IsNullOrWhiteSpace(sortOrder))
        {
            return categories;
        }
        if (sortOrder.ToLower() == "desc")
        {
            return categories.OrderByDescending(c => c.CategoryId);
        }

        return categories.OrderBy(c => c.CategoryId);
    }
    public static IQueryable<Category> SortByOptions(this IQueryable<Category> categories, string? sortBy,
        string? sortOrder)
    {
        if (string.IsNullOrWhiteSpace(sortBy))
        {
            return categories;
        }
        Expression<Func<Category,object>> keySelector = sortBy.ToLower() switch
        {
            "name" => c => c.CategoryName,
            "slug" => c => c.Slug,
            _ => c => c.CategoryId
        };
        if (!string.IsNullOrWhiteSpace(sortOrder) && sortOrder.ToLower() == "desc")
        {
            return categories.OrderByDescending(keySelector);
        }
        return categories.OrderBy(keySelector);
    }
}