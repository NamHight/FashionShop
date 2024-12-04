using System.Linq.Expressions;
using FashionShop.Context;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Repositories;

public abstract class GenericRepo<T> : IGenericRepo<T> where T : class
{
    protected readonly MyDbContext _context;
    
    public GenericRepo(MyDbContext context)
    {
        this._context = context;
    }
    
    public IQueryable<T> FindAll(bool trackChanges)
    {
        return trackChanges ? _context.Set<T>() : _context.Set<T>().AsNoTracking();
    }
    public async Task<List<T>> PageLinkAsync(int page, int pageSize, bool trackChanges)
    {
        return await FindAll(trackChanges)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    public IQueryable<T> FindById(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return trackChanges ? _context.Set<T>().Where(expression) : _context.Set<T>().Where(expression).AsNoTracking();
    }
  
    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }
    
    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
    
    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}