using System.Collections.Immutable;
using AutoMapper;
using FashionShop_API.Context;
using FashionShop_API.Dto;
using FashionShop_API.Models;
using FashionShop_API.Repositories.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FashionShop_API.Repositories.Categories;

public class RepositoryCategory : RepositoryBase<Category> , IRepositoryCategory
{
    private readonly IMapper _mapper;
    public RepositoryCategory(MyDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }
    public async Task<List<ResponseCategoryChildrenDto>> GetAllCategoriesAsync(bool trackChanges)
    {
        var categories = await FindAll(trackChanges)
            .Where(c => c.ParentId == null)
            .GroupJoin(_context.Categories, parent => parent.CategoryId, child => child.ParentId, (parent, children) =>
                 new ResponseCategoryChildrenDto
                {
                    Name = parent.CategoryName,
                    Categories = _mapper.Map<IEnumerable<ResponseCategoryDto>>(children)
                }
            ).ToListAsync();
        return categories;
    }
    
    public async Task<Category?> GetCategoryByIdAsync(long id, bool trackChanges)
    {
        var category = await FindByCondition(c => c.CategoryId.Equals(id),trackChanges).FirstOrDefaultAsync();
        return category;
    }
    
    public async Task<PagedListAsync<Category>> GetAllPaginatedAsync(int page, int limit)
    {
        var categories = await PagedListAsync<Category>.ToPagedListAsync(_context.Categories.AsQueryable(),page,limit);
        return categories;
    }

    public async Task<PagedListAsync<Category>> GetAllPaginatedAndSearchAndSortAsync(string? searchTearm,
        int page, int limit, string? sortBy,string? sortOrder)
    {
        var query = _context
            .Categories
            .SearchByName(searchTearm)
            .SortById(sortOrder)
            .SortByOptions(sortBy, sortOrder);
        var categories = await PagedListAsync<Category>.ToPagedListAsync(query, page, limit);
        return categories;
    }
    
    public async Task CreateAsync(Category category)
    {
        await Create(category);
    }

    public async Task<bool> CheckCategorySlugExistsAsync(string slug)
    {
        var checkSlug = await _context.Categories.Where(p => p.Slug.Equals(slug)).AsNoTracking().FirstOrDefaultAsync();
        return checkSlug is not null;
    }
}