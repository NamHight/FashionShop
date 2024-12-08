using System.Linq.Expressions;

namespace FashionShop.Repositories;

public interface IGenericRepo <T>
{
    IQueryable<T> FindAll(bool trackChanges);
    IQueryable<T> PageLinkAsync(int page, int pageSize, bool trackChanges);
    IQueryable<T> FindById(Expression<Func<T, bool>> expression, bool trackChanges);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> Count();
}