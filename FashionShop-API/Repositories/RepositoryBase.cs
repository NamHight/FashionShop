﻿using System.Linq.Expressions;
using FashionShop_API.Context;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly MyDbContext _context;
    
    public RepositoryBase(MyDbContext context)
    {
        _context = context;
    }
    public IQueryable<T> FindAll(bool trackChanges)
    {
        return trackChanges ? _context.Set<T>() :  _context.Set<T>().AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
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