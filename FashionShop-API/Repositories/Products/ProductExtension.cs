using FashionShop_API.Models;

namespace FashionShop_API.Repositories.Products;

public static class ProductExtension
{

    public static IQueryable<Product> SearchByName(this IQueryable<Product> products, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return products;
        }

        var searchResult = products.Where(c => c.ProductName.Contains(searchTerm));
        return searchResult;
    }

    public static IQueryable<Product> SortByCreatedDate(this IQueryable<Product> products, string? sortOrder)
    {
        if (string.IsNullOrWhiteSpace(sortOrder))
        {
            return products;
        }
        if (sortOrder.ToLower() == "desc")
        {
            return products.OrderByDescending(p => p.CreatedAt);
        }
        return products.OrderBy(p => p.CreatedAt);
    }

    public static IQueryable<Product> SortByPrice(this IQueryable<Product> products)
    {
        return products.OrderBy(p => p.Price);
    }
    public static IQueryable<Product> FilterProductPrice(this IQueryable<Product>
        products, decimal? minPrice, decimal? maxPrice) { 

        if (minPrice is null && maxPrice is null) {
            return products;
        }
        return products.Where(e => e.Price >= minPrice && e.Price <= maxPrice);
    }
    public static IQueryable<Product> FilterProductCategory(this IQueryable<Product>
       products, string categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
        {
            return products;
        }
        return products.Where(e => e.Category.CategoryName.ToLower().Contains(categoryName.ToLower()));
    }
}